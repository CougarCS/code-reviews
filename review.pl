#!/usr/bin/env perl

# Config
# If the detected user is incorrect, set the $USER variable below.
my $USER = "";

## Do not edit below this line.

use strict;
use warnings;

use FindBin;
use IPC::Open2;

# Need to be in the top of the git directory.
BEGIN { chdir $FindBin::Bin; }

# Constants
use constant UPSTREAM_URL => "https://github.com/CougarCS/code-reviews.git";

=head2 git

    git( @git_command_line_arguments )

Runs the git command with the given list of arguments and returns the lines of
output as an array.

=cut
sub git {
	my (@args) = @_;
	my ($out, $in);
	open2( $out, $in, 'git', @args );
	my @buffer;
	while( <$out> ) {
		chomp;
		push @buffer, $_
	}
	@buffer;
}

=head2 diag_out

    diag_out( @output )

Outputs each of the elements of the C<@output> array to C<STDERR>.

=cut
sub diag_out {
	print STDERR "  # $_\n" for @_;
}

=head2 git_diag

    git_diag( @git_command_line_arguments )

Calls C<git()> and outputs the command being run with C<diag_out()>.

=cut
sub git_diag {
	my (@args) = @_;
	diag_out "    \$ git @args";
	git(@args);
}

=head2 init

    init()

Makes sure that the upstream remote is available and finds the Github username
of the repo owner if it exists.

=cut
sub init {
	if( not grep { /upstream/ } git(qw(remote -v)) ) {
		diag_out "Adding upstream remote at <@{[UPSTREAM_URL]}>";
		git_diag(qw(remote add upstream), UPSTREAM_URL);
	}

	unless( length $USER ) {
		# get the username:
		# has to be between either a semi-colon or slash and after the
		# github.com domain name
		my ($origin) = grep { /\borigin\b/ } git(qw(remote -v));
		my ($found_user) = ( $origin =~ m,.* github.com[:/]  ([^/]*) ,x );
		diag_out "Detected GitHub user: $found_user";
		$USER = $found_user;
	}

	if( $USER =~ /CougarCS/i ) {
		die <<EOF
WARNING:

Repo owner can not be CougarCS. Please fork

    <@{[UPSTREAM_URL]}>

and run

    \$ git clone git\@github.com:/<GITHUB_USER>/code-reviews.git
EOF
	}
}

sub list_and_choose {
}

sub get_branches { grep { s,[* ],,g } git(qw(branch --list)); }

sub get_current_branch { grep { s,[* ],,g } grep { /\*/ } git(qw(branch --list)); }

sub action_loop {
	my $actions = [
		"Choose a problem to work on",
		"Update list of problems",
	];
	while(1) {
		diag_out( "Current problem: " );
		list_and_choose();
	}
}

init;
diag_out get_branches;
diag_out get_current_branch;

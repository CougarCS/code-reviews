#!/usr/bin/env perl

# Config
# If the detected user is incorrect, set the $USER variable below.
my $USER = "";

## Do not edit below this line.
use strict;
use warnings;

use FindBin; # CORE
use IPC::Open2; # CORE
use Term::ReadKey; # Git dep

# Need to be in the top of the git directory.
BEGIN { chdir $FindBin::Bin; }

# Constants
use constant UPSTREAM_URL => "https://github.com/CougarCS/code-reviews.git";
use constant PROBLEM_LIST_FILE => 'prob.txt';

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

=head2 get_branches

    get_branches()

Returns a list of branches in the git repo.

=cut
sub get_branches {
	map { (my $s = $_) =~ s,[* ],,g; $s } git(qw(branch --list));
}


=head2 get_current_branch

    get_current_branch()

Returns the name of the current branch in the git repo.

=cut
sub get_current_branch {
	map { (my $s = $_) =~ s,[* ],,g; $s } grep { /\*/ } git(qw(branch --list));
}

sub get_list_of_problems {
	master_blaster( sub {
		my $prob_fh = IO::File->new( PROBLEM_LIST_FILE(), 'r');
		my @list;
		while( <$prob_fh> ) {
			chomp;
			my $line = $_;
			my @split_line = split ':', $line;
			my $info = {
				name        => $split_line[0],
				month       => $split_line[1],
				description => $split_line[2],
			};
			unshift @list, $info;
		}
		return \@list;
	});
}

=head2 master_blaster

    master_blaster( $callback )

Runs the $callback coderef in the master branch and goes back to current branch
afterwards. Returns the results of C<$callback->()>.

=cut
sub master_blaster {
	# You found my Easter egg. This is a more fun name than run_in_master().
	#
	# :-P
	#
	# - zaki
	my ($cb) = @_;
	my $current_branch = get_current_branch();
	git(qw(stash));
	git(qw(checkout master));
	my @return = $cb->();
	git(qw(checkout), $current_branch);
	git(qw(stash pop));
	@return;
}

=head2 update_repo

    update_repo()

Pulls updates into the master branch from upstream/master and pushes to origin/master

=cut
sub update_repo {
	master_blaster(sub {
		diag_out git_diag(qw(pull upstream master));
		git_diag(qw(push origin master));
	});
}

sub action_loop {
	# dispatch table
	my $actions = {
		1 => {
			text => "Choose a problem to work on",
			action => sub { choose_problem() },
		},
		2 => {
			text => "Update list of problems",
			action => sub { update_repo() },
		},
		3 => {
			text => "Submit problem",
			action => sub { submit_problem() },
		},
		4 => {
			text => "Exit",
		},
	};
	while(1) {
		my $current_branch = get_current_branch();
		diag_out( "Current problem: "
			. ( $current_branch eq 'master'
				? 'None chosen'
				: $current_branch ) );
		my $choice = list_and_choose($actions);
		if( $actions->{$choice}{text} eq 'Exit' ) {
			last;
		}
		$actions->{$choice}{action}->();
	}
}

sub submit_problem {
	my $current_branch = get_current_branch();
	git_diag(qw(push -u origin), $current_branch);
	diag_out("Go to <https://github.com/CougarCS/code-reviews/pulls> and make a new pull request using the branch name $current_branch.");
}

sub switch_problem {
	my ($prob_name) = @_;
	my $branch_name = "$prob_name/$USER";
	unless( grep { /^$branch_name$/ } get_branches() ) {
		git(qw(branch), $branch_name);
	}
	git(qw(checkout), $branch_name);
}

sub choose_problem {
	my $problems = get_list_of_problems();
	my $actions;
	my $idx = 1;
	for my $prob (@$problems) {
		my $name = $prob->{name};
		$actions->{$idx} = {
			text => "$prob->{name} : $prob->{description}",
			action => sub {
				switch_problem($name);
			},
		};
		$idx++;
	}
	my $choice = list_and_choose( $actions );
	$actions->{$choice}{action}->();
}

sub list_and_choose {
	my ($actions) = @_;
	my @keys = sort keys %$actions;
	for my $k (@keys) {
		print "$k) $actions->{$k}{text}\n";
	}

	my $key;
        ReadMode 4; # Turn off controls keys
        while (not defined ($key = ReadKey(-1))) {
            # No key yet
        }
        ReadMode 0; # Reset tty mode before exiting
	$key;
}

# MAIN

init;
action_loop;

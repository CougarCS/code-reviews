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

sub git {
	my (@args) = @_;
	my ($out, $in);
	open2( $out, $in, 'git', @args );
	my @buffer;
	push @buffer, $_ while( <$out> );
	@buffer;
}

sub diag_out {
	print STDERR "  # @_\n";
}

sub git_diag {
	my (@args) = @_;
	diag_out "    \$ git @args";
	git(@args);
}

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
}

init;

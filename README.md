Code reviews!
=============

## Instructions

0. Install git.

   On Unix, Mac OS X, use your package manager.

   On Windows, install [Git Bash](http://git-scm.com/download/win). You can type your git commands in the Git
   Bash shell.

   If you are new to Git, go through the tutorial at <https://try.github.io/>.

   Create a GitHub account.

1. Go to <https://github.com/CougarCS/code-reviews>. Click fork.

2. Clone your fork:
   
	    git clone https://github.com/[YOUR USERNAME]/code-reviews

3. Open up a bash prompt (Git Bash on Windows). Change your directory to the
   cloned repository:

	    cd code-reviews

4a. Run the command:
   
	   ./review.pl

   This will present a menu that will help you make pull requests on separate
   branches for each problem.

4b. If you understand how to branch, you can choose not to use `review.pl`.
    Use the convention of {problem name}/{Github username}.

   For example,

	   # Github username is foo
	   git clone https://github.com/foo/code-reviews
	   cd code-reviews
	   git checkout -b prob01-poker/foo
	   # solve problem
	   git push origin prob01-poker/foo

5. Submit a pull request by going to your pull request page or go to

	    <https://github.com/<GITHUB USER>/code-reviews/compare/>

   and compare against the branch for the problem.

Optional step: Watch the <https://github.com/CougarCS/code-reviews> repository
to recieve all code reviews for everyone in your e-mail.

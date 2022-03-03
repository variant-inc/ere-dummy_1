#!/bin/bash

MICROSERVICE_REPO=git@github.com:variant-inc/ticket-type-microservice-template.git
MICROSERVICE_REMOTE_NAME=microservice-template
TEMP_BRANCH_NAME=temp-tag-branch

printf "\n##############################################"
printf "\n### Microservice Update"
printf "\n###"
printf "\n### This will update this microservice with"
printf "\n### the given tag from the microservice"
printf "\n### template repository."
printf "\n##############################################"
printf "\n\nUsage:"
printf "\n\n./scripts/update-from-template.sh x.x.x"
printf "\n\nExample: ./scripts/update-from-template.sh 1.2.3\n\n"

if [[ $# -ne 1 ]]
then
  printf "\nYou MUST pass a Git tag set in the Microservice Template"
  printf "\nrepository you want to update to.\n"
  exit 1
fi

TAG=$1
FOUND=0
while IFS= read -r line
do
    if [[ $line == "microservice-template" ]]
    then
      FOUND=1
    fi
done < <(git remote show)

if [[ $FOUND -eq 0 ]]
then
  printf "\nThe remote for the microservice template has not been added yet."
  printf "\nAdding now...\n"
  git remote add $MICROSERVICE_REMOTE_NAME $MICROSERVICE_REPO
  printf "\nDone.\n"
fi

ERR=$?

if [[ $ERR -gt 0 ]]
then
  exit $ERR
fi

printf "\nCheckout the main branch...\n"
git checkout master
git pull origin master

ERR=$?

if [[ $ERR -gt 0 ]]
then
  exit $ERR
fi

printf "\nFetching tag $TAG from the microservice-template remote...\n"
git fetch $MICROSERVICE_REMOTE_NAME refs/tags/$TAG:refs/tags/$TAG
ERR=$?

if [[ $ERR -gt 0 ]]
then
  exit $ERR
fi

TEXT=$(cat <<-REBASE
Found the tag $TAG.

!!! About to rebase the main branch with $TAG.
This may result in 1 or more conflicts.
This may also result in being in a detached head state.
In these cases Git provides the information about what
files are in conflict & what the state of the rebase is.

Resolve conflicts manually being sure to use any incoming
changes from the new tag over any local changes. If you
need your local changes stash them first.

If you run \`git status\` you should see something like:
...
You are currently rebasing.
    (all conflicts fixed: run "git rebase --continue")

Run the \`git rebase --continue\` command once you have
resolved all conflicts.

If you\'re now in a detached HEAD state then run:
  - git checkout -b $TAG-Branch
  - git checkout master
  - git merge --no-ff $TAG-Branch
  // Remove the rebase branch
  - git -D $TAG-Branch

The rebase should be complete now & you should have all the
latest changes from the template.

REBASE
)

echo "$TEXT"

printf "\nContinue? [Y/n]:\n"
read CONTINUE

if [[ -z $CONTINUE ]]
then
  CONTINUE=Y
fi

if [[ $CONTINUE != "Y" ]]
then
  printf "\nL8tr Sk8tr!\n"
  exit 0
fi

git rebase -s ort -Xtheirs master $TAG
ERR=$?

if [[ $ERR -gt 0 ]]
then
  exit $ERR
fi

exit 0
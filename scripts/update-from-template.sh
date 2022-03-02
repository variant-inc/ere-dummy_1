#!/bin/bash

MICROSERVICE_REPO=git@github.com:variant-inc/ticket-type-microservice-template.git
MICROSERVICE_REMOTE_NAME=microservice-template

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
  exit 0
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

#git remote show

printf "\nFetching tag $TAG using the microservice-template remote...\n"
git fetch $MICROSERVICE_REMOTE_NAME refs/tags/$TAG:refs/tags/$TAG
ERR=$?

if [[ $ERR -gt 0 ]]
then
  exit 0
fi

printf "\nFound the tag $TAG\nAttempting to merge $TAG in to your main branch...\n"
git merge --progress -s ort -Xtheirs $TAG --allow-unrelated-histories
git diff $TAG

exit $?
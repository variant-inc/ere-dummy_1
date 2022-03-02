#!/bin/bash

PROJECTNAME=TicketType.Microservice.Template

printf "\n#######################################"
printf "\n# Ticket Type Microservice Setup"
printf "\n#######################################\n\n"

printf "Working in %s" $(pwd)
printf "\nThis should be the repository root.\nContinue? [Y|n]:\n"
read CONTINUE

if [[ -z $CONTINUE ]]
then
  CONTINUE=Y
fi

if [[ $CONTINUE != 'Y' ]]
then
  printf "L8tr Sk8tr!\n"
  exit 0
fi

printf "Removing temporary Terraform files..."
printf "\nterraform/$PROJECTNAME/temp_*.tf"
printf "\nREADME.md\n"
rm terraform/$PROJECTNAME/temp_*.tf
rm README.md

printf "\n\nCopying README.md\n"
cp scripts/README.md.tmpl README.md

exit $?
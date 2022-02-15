#!/bin/sh
export RUNARGS=""
export PROJECT=""

while [ $# -gt 0 ]; do
key="${1}"
case $key in
  -p|--project)
    PROJECT=$2
    shift
    shift
    ;;
  --)
    shift
    APPARGS=$@
    break
    ;;
  *)
    RUNARGS="${RUNARGS} ${1}"
    shift
    ;;
esac
done

if [ -z "${PROJECT}" ]; then
  echo "Set --project to absolute (linux) path of desired project" 1>&2;
  exit 1
fi

set +e
for f in /docker-entrypoint-initvariant.d/*; do
  case "$f" in
    *.sh) "$0: running $f"; . "${f}" ;;
    *) "$0: ignoring $f" ;;
  esac
done
set -e

exec dotnet watch -p ${PROJECT} run ${RUNARGS} -- ${APPARGS}
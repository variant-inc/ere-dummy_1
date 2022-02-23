#!/bin/sh

LOCALSTACK_HOST=localstack
LOCALSTACK_PORT=4566
LOCALSTACK_SQS_PORT=4576

wait_for_queue() {
  printf "Waiting for queues...\n"

  PROJECT_NAME=$1
  QUEUE_NAME=$2
  
  if echo "${PROJECT}" | grep -i "${PROJECT_NAME}"; then
    for n in 1 2 3 4 5 6 7 8 9 10; do
      sleep 6
      printf 'GET /?Action=ListQueues HTTP/1.1\r\n\r\nHOST: localstack' | nc -v ${LOCALSTACK_HOST} ${LOCALSTACK_SQS_PORT} | grep "${QUEUE_NAME}" > /dev/null
      if [ $? -eq 0 ]; then
        echo "Found ${QUEUE_NAME}"
        break
      fi

      if [ $n -eq 10 ]; then
        echo "Could not find queue: ${QUEUE_NAME}" 1>&2;
        exit 5;
      fi
    done
  fi
}

wait-for-it ${LOCALSTACK_HOST}:${LOCALSTACK_PORT}
wait-for-it ${LOCALSTACK_HOST}:${LOCALSTACK_SQS_PORT}

wait_for_queue TicketType.Microservice.Template entity-queue
resource "aws_sqs_queue" "incoming_exceptions_queue" {
  name                      = local.sqs_incoming_queue_name
  kms_master_key_id         = data.aws_kms_alias.sns_key.id
  message_retention_seconds = var.entity_queue_retention_seconds
}

resource "aws_sqs_queue_policy" "incoming_exceptions_queue_policy" {
  policy    = data.aws_iam_policy_document.ticket_type_microservice_template_policy_document.json
  queue_url = aws_sqs_queue.incoming_exceptions_queue.id
}

resource "aws_sns_topic_subscription" "ticketing_handler_subscription" {
  endpoint             = aws_sqs_queue.incoming_exceptions_queue.arn
  protocol             = "sqs"
  topic_arn            = aws_sns_topic.incoming_exceptions_topic.arn
  raw_message_delivery = true
  depends_on           = [aws_sqs_queue_policy.incoming_exceptions_queue_policy]
}

# This file can go away. It is only needed for testing.
resource "aws_sqs_queue" "temp_outgoing_deadletter_queue" {
#  name                        = local.sqs_entity_deadletter_queue_name
  name                        = "eng-temp_outgoing_deadletter"
  kms_master_key_id           = data.aws_kms_alias.sns_key.id
}

# SQS Entity API queue AWS permissions policy.
data "aws_iam_policy_document" "temp_outgoing_queue_policy_data" {
#  policy_id = local.sqs_incoming_queue_name
  policy_id = "temp_outgoing_policy"
  version   = var.aws_policy_version
  statement {
    effect    = "Allow"
    resources = [aws_sqs_queue.temp_outgoing_queue.arn]
    actions   = ["sqs:SendMessage"]

    principals {
      identifiers = ["*"]
      type        = "*"
    }

    condition {
      test     = "ArnEquals"
      values   = [aws_sns_topic.outgoing_exceptions_topic.arn]
      variable = "aws:SourceArn"
    }
  }
}

resource "aws_sqs_queue" "temp_outgoing_queue" {
#  name                        = local.sqs_incoming_queue_name
  name                        = "eng-temp_outgoing_queue"
  kms_master_key_id           = data.aws_kms_alias.sns_key.id
  visibility_timeout_seconds  = 3
  message_retention_seconds   = 60

  redrive_policy = jsonencode({
    deadLetterTargetArn = aws_sqs_queue.temp_outgoing_deadletter_queue.arn
    maxReceiveCount     = 5
  })
}

resource "aws_sqs_queue_policy" "temp_outgoing_queue_policy" {
  policy    = data.aws_iam_policy_document.temp_outgoing_queue_policy_data.json
  queue_url = aws_sqs_queue.temp_outgoing_queue.id
}

resource "aws_sns_topic_subscription" "outgoing_topic_subscription" {
  endpoint             = aws_sqs_queue.temp_outgoing_queue.arn
  protocol             = "sqs"
  topic_arn            = aws_sns_topic.outgoing_exceptions_topic.arn
  raw_message_delivery = true
  depends_on           = [aws_sqs_queue_policy.temp_outgoing_queue_policy]
}
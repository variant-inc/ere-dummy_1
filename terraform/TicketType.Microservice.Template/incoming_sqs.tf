data "aws_kms_alias" "incoming_sns_key" {
  name = var.kms_key_alias_incoming_sns
}

resource "aws_sqs_queue" "incoming_exceptions_queue" {
  name                      = local.kebab_env_name
  kms_master_key_id         = data.aws_kms_alias.incoming_sns_key.id
  message_retention_seconds = var.entity_queue_retention_seconds
  tags = module.tags.tags
}

resource "aws_sqs_queue_policy" "incoming_exceptions_queue_policy" {
  policy    = data.aws_iam_policy_document.outgoing_exceptions_policy.json
  queue_url = aws_sqs_queue.incoming_exceptions_queue.id
}

data "aws_iam_policy_document" "outgoing_exceptions_policy" {
  policy_id = local.kebab_env_name
  version   = "2012-10-17"
  statement {
    effect    = "Allow"
    resources = [aws_sqs_queue.incoming_exceptions_queue.arn]
    actions   = ["sqs:SendMessage"]

    principals {
      identifiers = ["*"]
      type        = "*"
    }

    condition {
      test     = "ArnEquals"
      values   = [data.aws_sns_topic.incoming_exceptions_topic.arn]
      variable = "aws:SourceArn"
    }
  }
}

resource "aws_sns_topic_subscription" "ticketing_handler_subscription" {
  endpoint             = aws_sqs_queue.incoming_exceptions_queue.arn
  protocol             = "sqs"
  topic_arn            = data.aws_sns_topic.incoming_exceptions_topic.arn
  raw_message_delivery = true
  depends_on           = [aws_sqs_queue_policy.incoming_exceptions_queue_policy]
}

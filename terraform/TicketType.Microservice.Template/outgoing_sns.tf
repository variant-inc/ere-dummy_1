// Create outgoing topic
data "aws_sns_topic" "outgoing_exceptions_topic" {
  name = var.outgoing_exceptions_topic_name
}

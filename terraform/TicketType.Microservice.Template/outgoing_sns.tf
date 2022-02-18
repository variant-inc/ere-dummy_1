// Create outgoing topic
data "aws_sns_topic" "outgoing_exceptions_topic" {
  name = var.ticketing_api_outgoing_topic
}

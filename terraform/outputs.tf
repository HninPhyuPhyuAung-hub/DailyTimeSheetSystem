# Outputs for the three-region VPC architecture

# Singapore VPC Outputs
output "singapore_vpc_id" {
  description = "The ID of the Singapore VPC"
  value       = module.vpc_singapore.vpc_id
}

output "singapore_public_subnet_ids" {
  description = "List of IDs of public subnets in Singapore"
  value       = module.vpc_singapore.public_subnet_ids
}

output "singapore_private_subnet_ids" {
  description = "List of IDs of private subnets in Singapore"
  value       = module.vpc_singapore.private_subnet_ids
}

output "singapore_nat_gateway_id" {
  description = "ID of the NAT Gateway in Singapore"
  value       = module.vpc_singapore.nat_gateway_id
}

# Korea VPC Outputs
output "korea_vpc_id" {
  description = "The ID of the Korea VPC"
  value       = module.vpc_korea.vpc_id
}

output "korea_public_subnet_ids" {
  description = "List of IDs of public subnets in Korea"
  value       = module.vpc_korea.public_subnet_ids
}

output "korea_private_subnet_ids" {
  description = "List of IDs of private subnets in Korea"
  value       = module.vpc_korea.private_subnet_ids
}

output "korea_nat_gateway_id" {
  description = "ID of the NAT Gateway in Korea"
  value       = module.vpc_korea.nat_gateway_id
}

# Japan VPC Outputs
output "japan_vpc_id" {
  description = "The ID of the Japan VPC"
  value       = module.vpc_japan.vpc_id
}

output "japan_public_subnet_ids" {
  description = "List of IDs of public subnets in Japan"
  value       = module.vpc_japan.public_subnet_ids
}

output "japan_private_subnet_ids" {
  description = "List of IDs of private subnets in Japan"
  value       = module.vpc_japan.private_subnet_ids
}

output "japan_nat_gateway_id" {
  description = "ID of the NAT Gateway in Japan"
  value       = module.vpc_japan.nat_gateway_id
}

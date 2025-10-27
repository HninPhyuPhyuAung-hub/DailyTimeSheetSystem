# Three-Region VPC Architecture

## Overview
This document provides a detailed explanation of the three-region VPC architecture deployed across Singapore, South Korea, and Japan.

## Architecture Components

### 1. Regional VPCs

#### Singapore Region (ap-southeast-1)
- **VPC CIDR**: 10.0.0.0/16
- **Public Subnets**: 
  - 10.0.1.0/24 (ap-southeast-1a)
  - 10.0.2.0/24 (ap-southeast-1b)
- **Private Subnets**:
  - 10.0.101.0/24 (ap-southeast-1a)
  - 10.0.102.0/24 (ap-southeast-1b)

#### South Korea Region (ap-northeast-2)
- **VPC CIDR**: 10.1.0.0/16
- **Public Subnets**:
  - 10.1.1.0/24 (ap-northeast-2a)
  - 10.1.2.0/24 (ap-northeast-2c)
- **Private Subnets**:
  - 10.1.101.0/24 (ap-northeast-2a)
  - 10.1.102.0/24 (ap-northeast-2c)

#### Japan Region (ap-northeast-1)
- **VPC CIDR**: 10.2.0.0/16
- **Public Subnets**:
  - 10.2.1.0/24 (ap-northeast-1a)
  - 10.2.2.0/24 (ap-northeast-1c)
- **Private Subnets**:
  - 10.2.101.0/24 (ap-northeast-1a)
  - 10.2.102.0/24 (ap-northeast-1c)

### 2. Networking Components

#### Internet Gateway (IGW)
- One per region
- Provides internet connectivity for public subnets
- Allows bidirectional internet traffic for resources in public subnets

#### NAT Gateway
- One per region (deployed in first public subnet)
- Provides outbound internet connectivity for private subnets
- Includes Elastic IP for consistent public IP addressing
- Allows private subnet resources to access internet while remaining private

#### Route Tables

**Public Route Table (per region)**
- Default route: 0.0.0.0/0 → Internet Gateway
- Associated with all public subnets
- Enables direct internet access

**Private Route Table (per region)**
- Default route: 0.0.0.0/0 → NAT Gateway
- Associated with all private subnets
- Enables outbound internet access through NAT

### 3. Security Groups

#### Web Security Group
- **Inbound Rules**:
  - HTTP (80): 0.0.0.0/0
  - HTTPS (443): 0.0.0.0/0
  - SSH (22): 0.0.0.0/0
- **Outbound Rules**:
  - All traffic: 0.0.0.0/0

#### Database Security Group
- **Inbound Rules**:
  - MySQL (3306): From Web Security Group
  - PostgreSQL (5432): From Web Security Group
- **Outbound Rules**:
  - All traffic: 0.0.0.0/0

### 4. High Availability Design

#### Multi-AZ Deployment
- Each region uses 2 Availability Zones
- Subnets distributed across AZs for fault tolerance
- Enables deployment of HA services (ELB, RDS Multi-AZ)

#### Redundancy
- Multiple subnets per tier (public/private)
- Independent networking in each region
- No single point of failure within a region

### 5. Route53 Geolocation Routing (Optional)

When enabled with a domain name:
- **Singapore**: Serves users from Singapore and Southeast Asia
- **South Korea**: Serves users from South Korea
- **Japan**: Serves users from Japan
- **Default**: Singapore serves all other locations

#### Health Checks
- HTTP health checks for each region
- Automatic failover to healthy regions
- 30-second check interval
- 3 consecutive failures trigger unhealthy state

## Traffic Flow

### Public Subnet Traffic
1. Resource in public subnet initiates request
2. Traffic routes through Internet Gateway
3. Public IP allows direct internet communication
4. Response returns through IGW to resource

### Private Subnet Traffic (Outbound)
1. Resource in private subnet initiates outbound request
2. Traffic routes to NAT Gateway in public subnet
3. NAT Gateway translates private IP to public IP (EIP)
4. Traffic exits through Internet Gateway
5. Response returns through IGW → NAT → private resource

### Inter-Subnet Communication
1. Resources in same VPC can communicate using private IPs
2. Security groups control access between subnets
3. Network ACLs provide additional subnet-level filtering

## Deployment Strategy

### Phase 1: Network Foundation
1. Create VPCs in all three regions
2. Deploy subnets in multiple AZs
3. Configure Internet Gateways
4. Set up NAT Gateways with Elastic IPs

### Phase 2: Routing Configuration
1. Create and configure route tables
2. Associate subnets with appropriate route tables
3. Verify routing paths

### Phase 3: Security Configuration
1. Create security groups
2. Define security group rules
3. Test connectivity and security policies

### Phase 4: Application Deployment (Future)
1. Deploy EC2 instances or containers
2. Configure load balancers
3. Set up databases in private subnets
4. Implement auto-scaling

### Phase 5: DNS and Routing (Future)
1. Configure Route53 hosted zone
2. Set up health checks
3. Create geolocation routing policies
4. Test failover scenarios

## Best Practices Implemented

1. **Network Segmentation**: Separate public and private subnets
2. **High Availability**: Multi-AZ deployment in each region
3. **Security**: Defense in depth with security groups
4. **Scalability**: Modular design supports easy expansion
5. **Cost Optimization**: Single NAT Gateway per region
6. **Documentation**: Comprehensive README and architecture docs
7. **Automation**: Infrastructure as Code with Terraform

## Use Cases

### Multi-Region Web Application
- Deploy web servers in public subnets
- Deploy databases in private subnets
- Use Route53 for geographic load distribution
- Implement read replicas across regions

### Disaster Recovery
- Primary region: Active workloads
- Secondary regions: DR/backup
- Automated failover with Route53
- Data replication between regions

### Global Content Delivery
- Application servers in each region
- CDN integration for static content
- Database replication for data consistency
- Low-latency access for regional users

### Compliance and Data Residency
- Keep data in specific geographic regions
- Comply with local regulations
- Control data flow between regions
- Audit and logging per region

## Monitoring and Operations

### Recommended Monitoring
1. **VPC Flow Logs**: Network traffic analysis
2. **CloudWatch Metrics**: NAT Gateway, network performance
3. **CloudTrail**: API activity and changes
4. **Route53 Health Checks**: Endpoint availability
5. **Cost Explorer**: Infrastructure costs

### Operational Tasks
1. Regular security group audits
2. Review VPC flow logs for anomalies
3. Monitor NAT Gateway data transfer costs
4. Test failover scenarios
5. Update route tables as needed
6. Review and optimize security policies

## Future Enhancements

1. **VPC Peering**: Connect VPCs across regions
2. **Transit Gateway**: Centralized routing between VPCs
3. **VPN Connections**: On-premises connectivity
4. **Direct Connect**: Dedicated network connection
5. **VPC Endpoints**: Private connections to AWS services
6. **Network Firewall**: Advanced threat protection
7. **WAF**: Web Application Firewall for ALB/CloudFront
8. **Shield**: DDoS protection

## Conclusion

This architecture provides a solid foundation for deploying multi-region applications with high availability, security, and scalability. The modular Terraform design allows for easy customization and extension based on specific requirements.

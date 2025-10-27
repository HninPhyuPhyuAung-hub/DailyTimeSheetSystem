# Multi-Region VPC Architecture with Terraform

This Terraform configuration creates a multi-region AWS infrastructure with VPCs in three regions: Singapore, South Korea, and Japan. Each region includes public and private subnets with proper routing, NAT gateways, and security groups.

## Architecture Overview

Based on the Route53 Geolocation Routing Policy lab architecture, this setup includes:

### Regions
- **Singapore** (ap-southeast-1) - CIDR: 10.0.0.0/16
- **South Korea** (ap-northeast-2) - CIDR: 10.1.0.0/16
- **Japan** (ap-northeast-1) - CIDR: 10.2.0.0/16

### Each Region Contains
1. **VPC** with dedicated CIDR block
2. **2 Public Subnets** in different Availability Zones
   - Map public IP on launch
   - Connected to Internet Gateway
3. **2 Private Subnets** in different Availability Zones
   - Connected to NAT Gateway for outbound internet access
4. **Internet Gateway** for public subnet internet access
5. **NAT Gateway** for private subnet outbound internet access
6. **Route Tables**
   - Public route table: Routes to Internet Gateway
   - Private route table: Routes to NAT Gateway
7. **Security Groups**
   - Web security group: Allows HTTP (80), HTTPS (443), SSH (22)
   - Database security group: Allows MySQL (3306) and PostgreSQL (5432) from web SG

### Network Architecture per Region

```
┌─────────────────────────────────────────────────────────┐
│                      VPC (10.x.0.0/16)                  │
│                                                          │
│  ┌──────────────────────┐  ┌──────────────────────┐    │
│  │  Public Subnet 1     │  │  Public Subnet 2     │    │
│  │  (10.x.1.0/24)       │  │  (10.x.2.0/24)       │    │
│  │  AZ-a                │  │  AZ-b/c              │    │
│  │  ┌──────────────┐    │  │                      │    │
│  │  │ NAT Gateway  │    │  │                      │    │
│  │  └──────────────┘    │  │                      │    │
│  └──────────┬───────────┘  └──────────┬───────────┘    │
│             │                          │                │
│         ┌───┴──────────────────────────┴───┐            │
│         │     Internet Gateway (IGW)       │            │
│         └───────────────┬──────────────────┘            │
│                         │                               │
│  ┌──────────────────────┴───────┬──────────────────┐   │
│  │  Private Subnet 1            │ Private Subnet 2 │   │
│  │  (10.x.101.0/24)             │ (10.x.102.0/24)  │   │
│  │  AZ-a                        │ AZ-b/c           │   │
│  └──────────────────────────────┴──────────────────┘   │
│                                                          │
└──────────────────────────────────────────────────────────┘
```

## Prerequisites

1. **AWS Account** with appropriate permissions
2. **Terraform** installed (>= 1.0)
3. **AWS CLI** configured with credentials

## File Structure

```
terraform/
├── main.tf                  # Main configuration with provider setup and module calls
├── variables.tf             # Input variables
├── outputs.tf              # Output values
├── route53.tf              # Route53 geolocation routing configuration (commented)
├── modules/
│   └── vpc/
│       ├── main.tf         # VPC module main configuration
│       ├── variables.tf    # VPC module variables
│       └── outputs.tf      # VPC module outputs
└── README.md               # This file
```

## Usage

### Step 1: Configure AWS Credentials

```bash
# Configure AWS CLI with your credentials
aws configure
```

Or set environment variables:
```bash
export AWS_ACCESS_KEY_ID="your-access-key"
export AWS_SECRET_ACCESS_KEY="your-secret-key"
export AWS_DEFAULT_REGION="ap-southeast-1"
```

### Step 2: Initialize Terraform

```bash
cd terraform
terraform init
```

### Step 3: Review the Plan

```bash
terraform plan
```

This will show you all the resources that will be created:
- 3 VPCs (one in each region)
- 6 Public subnets (2 per region)
- 6 Private subnets (2 per region)
- 3 Internet Gateways
- 3 NAT Gateways
- 3 Elastic IPs
- Route tables and associations
- Security groups

### Step 4: Apply the Configuration

```bash
terraform apply
```

Type `yes` when prompted to confirm.

### Step 5: View Outputs

After successful deployment, Terraform will output important information:
- VPC IDs for each region
- Subnet IDs (public and private)
- NAT Gateway IDs
- NAT Gateway public IPs

```bash
terraform output
```

## Resource Details

### VPC Module

The VPC module (`modules/vpc/`) is reusable and creates:
- VPC with DNS support enabled
- Internet Gateway
- Public and private subnets
- NAT Gateway with Elastic IP
- Route tables and associations
- Security groups for web and database tiers

### Variables

You can customize the deployment by modifying `variables.tf` or passing variables:

```bash
terraform apply -var="project_name=my-project" -var="environment=staging"
```

## Route53 Geolocation Routing

The `route53.tf` file contains a commented template for setting up Route53 with geolocation routing. To enable:

1. Uncomment the code in `route53.tf`
2. Set the `domain_name` variable
3. Deploy EC2 instances or load balancers in each region
4. Update the health check and record configurations with actual endpoints
5. Apply the configuration

The geolocation routing will direct users to the nearest region:
- Singapore users → Singapore VPC
- Korean users → Korea VPC
- Japanese users → Japan VPC
- Others → Default (Singapore)

## Cost Considerations

This infrastructure creates resources that incur costs:
- **NAT Gateways**: ~$0.045/hour per NAT Gateway (3 total)
- **Elastic IPs**: Free when attached to running instances
- **Data Transfer**: Varies by usage
- **VPCs and Subnets**: Free

**Estimated Monthly Cost**: ~$100-150 (primarily NAT Gateway costs)

To reduce costs:
- Use a single NAT Gateway per region (already configured)
- Remove NAT Gateway if private subnets don't need internet access
- Use VPC Endpoints for AWS services instead of NAT Gateway

## Cleanup

To destroy all resources:

```bash
terraform destroy
```

Type `yes` when prompted.

## Security Best Practices

1. **Security Groups**: Review and restrict security group rules based on your needs
2. **SSH Access**: Consider restricting SSH (port 22) to specific IP ranges
3. **VPC Flow Logs**: Enable for monitoring and troubleshooting
4. **Network ACLs**: Add additional network ACLs if needed
5. **Private Subnets**: Deploy sensitive resources in private subnets
6. **Secrets Management**: Never commit AWS credentials to version control

## Extending the Architecture

### Adding EC2 Instances

Create a new file `ec2.tf`:

```hcl
module "ec2_singapore" {
  source = "./modules/ec2"
  providers = {
    aws = aws.singapore
  }
  
  vpc_id            = module.vpc_singapore.vpc_id
  subnet_id         = module.vpc_singapore.public_subnet_ids[0]
  security_group_id = module.vpc_singapore.web_security_group_id
}
```

### Adding Load Balancers

Create Application Load Balancers in each region for high availability.

### Adding RDS Databases

Deploy RDS instances in private subnets using the database security groups.

### VPC Peering

Set up VPC peering between regions for cross-region communication.

## Troubleshooting

### Common Issues

1. **Insufficient Permissions**: Ensure your AWS credentials have permissions to create VPCs, subnets, and networking resources
2. **Region Limits**: Check AWS service quotas for your regions
3. **CIDR Conflicts**: Ensure CIDR blocks don't overlap if connecting VPCs
4. **NAT Gateway Limits**: Default limit is 5 per region

### Validation

Check resources in AWS Console:
```bash
# List VPCs
aws ec2 describe-vpcs --region ap-southeast-1
aws ec2 describe-vpcs --region ap-northeast-2
aws ec2 describe-vpcs --region ap-northeast-1

# List subnets
aws ec2 describe-subnets --region ap-southeast-1
```

## References

- [AWS VPC Documentation](https://docs.aws.amazon.com/vpc/)
- [Route53 Geolocation Routing](https://docs.aws.amazon.com/Route53/latest/DeveloperGuide/routing-policy-geo.html)
- [Terraform AWS Provider](https://registry.terraform.io/providers/hashicorp/aws/latest/docs)
- [Lab Reference](https://vcloudynet.blogspot.com/2017/02/lab-route53-geolocation-routing-policy.html?m=1)

## Support

For issues or questions:
1. Review the Terraform plan output
2. Check AWS CloudTrail for API errors
3. Review VPC Flow Logs
4. Consult AWS documentation

## License

This Terraform configuration is provided as-is for educational and deployment purposes.

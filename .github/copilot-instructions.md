# GitHub Actions Study Repository

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

This is a GitHub Actions study repository for learning GitHub Actions workflows. The repository contains example workflows demonstrating different GitHub Actions patterns and features.

## Working Effectively

### Prerequisites and Setup
- All required tools are pre-installed in the development environment:
  - `yamllint` for YAML syntax validation
  - `act` (v0.2.66+) for local GitHub Actions testing 
  - `gh` (GitHub CLI) for workflow management
  - `docker` for containerized action testing
  - `node` (v20+) and `npm` for JavaScript-based actions

### Essential Commands for GitHub Actions Development

#### Workflow Validation
- **ALWAYS validate YAML syntax first**: `yamllint .github/workflows/*.yml`
  - Expected warnings about document-start and truthy values are normal
  - Address syntax errors before testing workflows
- **List all available workflows**: `act --list` 
  - NOTE: This will fail if summary.yml contains syntax errors due to the experimental `models` permission
  - Temporarily move problematic workflows if needed: `mv .github/workflows/summary.yml .github/workflows/summary.yml.bak`

#### Local Testing with Act
- **Configure act on first use**: Select "Medium" image when prompted (takes 5-10 minutes to download)
- **Test workflow syntax**: `act --list --workflows .github/workflows/[workflow-name].yml`
- **Dry run workflows**: `act [event-type] --workflows .github/workflows/[workflow-name].yml -n`
  - Example: `act pull_request_target --workflows .github/workflows/greetings.yml -n`
  - NOTE: Dry runs may show "unsupported object type" errors for action references - this is expected
- **NEVER CANCEL**: Act operations can take 10-15 minutes for image downloads. Set timeout to 20+ minutes.

#### Workflow File Management
- **Create labeler configuration**: The label.yml workflow requires `.github/labeler.yml` config file
- **Check workflow permissions**: Ensure workflows have appropriate `permissions` sections
- **Validate action versions**: Use specific versions (e.g., `@v4`) not floating tags

## Repository Structure

### Workflows Overview
The repository contains 4 example workflows in `.github/workflows/`:

1. **greetings.yml** - Welcomes new contributors on first issue/PR
   - Triggers: `pull_request_target`, `issues`
   - Uses: `actions/first-interaction@v1`
   - Status: ✅ Functional with act

2. **label.yml** - Auto-labels PRs based on file paths  
   - Triggers: `pull_request_target`
   - Uses: `actions/labeler@v4`
   - Requires: `.github/labeler.yml` configuration file
   - Status: ✅ Functional with act

3. **stale.yml** - Marks stale issues and PRs
   - Triggers: `schedule` (cron: '29 7 * * *')
   - Uses: `actions/stale@v5`
   - Status: ✅ Functional with act

4. **summary.yml** - AI-powered issue summarization
   - Triggers: `issues` (opened)
   - Uses: `actions/ai-inference@v1` (experimental)
   - Status: ⚠️ Contains experimental `models` permission that act cannot parse

### Configuration Files
- `.github/labeler.yml` - Path-based labeling rules for the labeler workflow
- `.github/workflows/` - All GitHub Actions workflow definitions

## Validation and Testing

### Mandatory Validation Steps
1. **ALWAYS run yamllint**: `yamllint .github/workflows/*.yml`
2. **ALWAYS test with act**: `act --list` and dry run new/modified workflows
3. **ALWAYS validate permissions**: Ensure workflows have minimal required permissions
4. **ALWAYS test locally**: Use `act [event] --workflows [workflow-file] -n` before committing

### Manual Testing Scenarios
- **For greeting workflow**: Cannot be fully tested locally (requires GitHub API tokens)
  - Dry run with: `act pull_request_target --workflows .github/workflows/greetings.yml -n`
  - Expected: "unsupported object type" error is normal for action references
- **For labeler workflow**: Requires `.github/labeler.yml` - verify config exists and is valid
  - Dry run with: `act pull_request_target --workflows .github/workflows/label.yml -n`
- **For stale workflow**: Schedule-based, can only validate syntax locally
  - Cannot dry run schedule events locally
- **For summary workflow**: Uses experimental AI features - syntax validation only
  - Contains experimental `models` permission that prevents act parsing

### Common Issues and Solutions
- **act parsing errors**: The summary.yml workflow uses experimental `models` permission that act cannot parse
  - Solution: Temporarily move it during testing: `mv .github/workflows/summary.yml .github/workflows/summary.yml.bak`
- **Missing labeler.yml**: The label workflow will fail without configuration
  - Solution: Create `.github/labeler.yml` with path-based rules
- **yamllint warnings**: Document-start and truthy value warnings are expected in GitHub Actions YAML
  - These warnings are normal and do not prevent workflow execution

### Expected Timing
- **yamllint validation**: < 5 seconds
- **act setup (first time)**: 5-10 minutes for image download. NEVER CANCEL.
- **act workflow listing**: 5-15 seconds 
- **act dry run**: 30-60 seconds per workflow
- **Set timeouts**: Use 20+ minute timeouts for act operations

## Development Workflow

### Adding New Workflows
1. Create `.github/workflows/new-workflow.yml`
2. Validate syntax: `yamllint .github/workflows/new-workflow.yml`
3. Test with act: `act --list --workflows .github/workflows/new-workflow.yml`
4. Dry run: `act [trigger-event] --workflows .github/workflows/new-workflow.yml -n`
5. Add any required configuration files (like labeler.yml)
6. Commit and test in GitHub environment

### Modifying Existing Workflows  
1. **ALWAYS backup**: `cp .github/workflows/workflow.yml .github/workflows/workflow.yml.bak`
2. Make minimal changes
3. Validate: `yamllint .github/workflows/workflow.yml`
4. Test: `act --list --workflows .github/workflows/workflow.yml`
5. Dry run if applicable
6. Remove backup if successful

### Best Practices
- **Use specific action versions**: `actions/checkout@v4` not `actions/checkout@main`
- **Minimal permissions**: Only grant permissions actually needed
- **Clear naming**: Use descriptive workflow and job names
- **Documentation**: Add comments explaining complex logic
- **Event targeting**: Use appropriate triggers for workflow purpose

## Common Tasks Reference

### Repository Root Structure
```
.
├── .git/
├── .github/
│   ├── workflows/
│   │   ├── greetings.yml
│   │   ├── label.yml  
│   │   ├── stale.yml
│   │   └── summary.yml
│   └── labeler.yml
└── README.md
```

### Workflow Status Quick Check
```bash
# Validate all workflows
yamllint .github/workflows/*.yml

# List runnable workflows (excludes summary.yml due to experimental features)
mv .github/workflows/summary.yml .github/workflows/summary.yml.bak
act --list
mv .github/workflows/summary.yml.bak .github/workflows/summary.yml

# Check specific workflow
act --list --workflows .github/workflows/greetings.yml

# Test workflow dry run (use correct event trigger)
act pull_request_target --workflows .github/workflows/greetings.yml -n
```

### Required Files for Each Workflow
- **greetings.yml**: No additional files required
- **label.yml**: Requires `.github/labeler.yml` configuration  
- **stale.yml**: No additional files required
- **summary.yml**: No additional files required (but uses experimental features)

This repository focuses on GitHub Actions learning rather than traditional application development. The "code" here consists of workflow definitions that automate repository management tasks.
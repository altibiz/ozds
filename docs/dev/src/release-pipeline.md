# OZDS Release Pipeline

## Overview

The OZDS project uses a three-branch deployment strategy:

- **`dev`** (default branch) - All development work, PRs merge here
- **`qa`** - Staging environment (RPi deployment, currently manual)
- **`release`** - Production environment (Azure App Service)

### Environments

- **Development**: Local dev shells (Nix) or manual dependency management
  (Windows)
- **Staging**: Raspberry Pi with PostgreSQL
- **Production**: Azure App Service with Azure SQL Database

## Pre-Release Checklist

Before starting a release:

- [ ] All intended features merged to `dev` branch
- [ ] CI pipeline passing on latest `dev` commit
- [ ] Any critical issues resolved
- [ ] Database migration strategy planned (if applicable)
- [ ] Downtime window communicated (if needed for long migrations)

## Step-by-Step Release Process

### 1. Prepare Release Branch

```bash
# Ensure dev has any hotfixes from release
git checkout dev
git rebase release
# Resolve any conflicts if they exist
# Create PR on dev branch with changelog updates
```

### 2. Deploy to Release

```bash
# After release PR is merged to dev
git checkout release
git rebase dev
git push origin release
```

### 3. Monitor Deployment

- Watch GitHub Actions logs for build/deploy status
- Check email for any failure notifications
- Monitor Azure App Service log stream during deployment

### 4. Verify Deployment

#### Check for Migration Requirements

- Monitor Azure App Service log stream
- Look for specific migration error messages
- If migration needed, proceed to Migration section

#### Post-Deployment Verification

- [ ] Visit production URL and test critical user flows
- [ ] Check Azure App Service logs for any errors
- [ ] Verify key functionality through manual testing
- [ ] Monitor Azure metrics for anomalies

## Migration Handling

### When Migrations Are Required

1. **Identify Migration Need**: App will throw specific error and shut down
2. **Prepare for Migration**:

- Ensure stable connection
- Have rollback plan ready
- Monitor Azure metrics

### Running Migrations

```bash
git checkout release
# Load environment variables
# (via direnv or manual .env setup)
just migrations migrate
```

**Important Notes:**

- All migrations run in a single transaction
- Long-running migrations (hours) are normal for timeseries data
- Monitor progress via Azure metrics
- Manual oversight required - no CI automation

### Migration Rollback

If migration fails:

1. Transaction will auto-rollback
2. Rollback Azure App Service deployment to previous version
3. Investigate issue before retry
4. Apply fixes and restart process

## Hotfix Procedures

### Critical Production Issues

1. **Immediate Fix**: Apply hotfix directly to `release` branch
2. **Deploy**: Push `release` branch (triggers automatic deployment)
3. **Monitor**: Watch deployment and verify fix
4. **Integrate**: During next release, rebase `dev` onto `release` to bring
   hotfixes back

### Hotfix Integration Flow

```bash
# During next release preparation
git checkout dev
git rebase release # Brings hotfixes into dev
# Resolve conflicts if any
# Continue with normal release process
```

## Monitoring and Alerting

### Production Health Monitoring

- **Azure Alerts**: Email notifications for critical thresholds
- **Database Monitoring**: 80% capacity alert (critical)
- **Application Logs**: Azure App Service log stream
- **Manual Checks**: Daily verification of key functionality

### Access Methods

- **Log Monitoring**: Azure App Service log stream
- **Direct Access**: SSH via Azure App Service frontend (when log stream is
  cluttered)
- **Application Access**: Direct URL testing

## Known Issues and Troubleshooting

### Staging Environment (RPi)

- **Issue**: RPi disappears from VPN after 3-7 days
- **Temporary Fix**: Restart RPi
- **Status**: Under investigation
- **Impact**: Staging deployments currently manual until resolved

### Long Migration Times

- **Expected**: Hours-long migrations for timeseries data are normal
- **Monitoring**: Use Azure metrics, no specific progress indicators
- **Timeout**: 30-minute Azure timeout requires manual migration approach

### Build Dependencies

- **Chromium Bundle**: 1GB deployment size due to Azure App Service restrictions

## Emergency Procedures

### Production Down

1. Check Azure App Service status
2. Review recent deployments
3. Check database connectivity
4. Review application logs
5. If migration-related, follow migration rollback procedure

### Database Issues

1. Check Azure SQL Database metrics
2. Verify connection strings
3. Check for long-running queries
4. Monitor disk space (80% alert threshold)

### Rollback Process

1. **Application**: Rollback Azure App Service to previous deployment
2. **Database**: Transaction-based rollback (automatic on migration failure)
3. **Verification**: Test critical functionality after rollback
4. **Investigation**: Identify root cause before next deployment attempt

## Future Improvements

### Planned Enhancements

- **E2E Testing**: Integration into CI pipeline (under evaluation)
- **QA Automation**: Automatic deployment to RPi on `qa` branch pushes
- **Auth Migration**: Remove Orchard Core, implement Auth0 or similar
  integration
- **VM Deployment**: Transition from Azure App Service to VM-based deployment

### Process Improvements

- **Staging Reliability**: Resolve RPi connectivity issues
- **Migration Automation**: Evaluate feasibility for shorter migrations
- **Monitoring Enhancement**: More granular application health checks

---

## Quick Reference Commands

```bash
# Development setup
just dev          # Start development environment

# Migration management
just migrations   # Run pending migrations

# Release preparation
git checkout dev
git rebase release
# Create release PR, merge to dev
git checkout release
git rebase dev
git push origin release
```

## Contact and Escalation

For release issues or questions:

- Check Azure alerts and logs first
- Review this documentation
- Escalate to senior team members if needed
- Document any new issues or solutions for future reference

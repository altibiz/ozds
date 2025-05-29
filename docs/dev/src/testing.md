# OZDS Testing Guidelines

## Overview

OZDS follows a comprehensive testing strategy designed around business
criticality and system architecture. Our approach prioritizes thorough testing
of financially critical components while maintaining practical development
velocity.

## Testing Architecture

### Test Project Organization

- **Core Logic Projects** → Unit Tests (e.g., `Ozds.Business.Test`)
- **Integration Projects** → Integration Tests
- **Startup/Frontend Projects** → End-to-End Tests

### Test Pyramid + Criticality Focus

We follow a modified test pyramid where test coverage intensity is determined by
business impact:

- **Critical Path**: Financial calculations, billing logic, regulatory
  compliance
- **Standard Path**: General business logic, data processing
- **Basic Path**: UI components, non-critical integrations

## Testing Strategies by Component Type

### Financial Calculations (Critical)

- **Approach**: Property-based testing with random data generation
- **Tools**: AutoFixture, Bogus
- **Method**: Implement calculation logic twice (source + test) and verify exact
  output matching
- **Goal**: Catch inconsistencies by forcing developers to think through logic
  multiple times

### Integration Testing

- **Scope**: Database operations and data persistence
- **Focus**: Verify data integrity between input and storage
- **Constraint**: Keep business logic OUT of integration projects
- **Example**: Test that data written to TimescaleDB matches input data exactly

### End-to-End Testing

- **Browser Automation**: Playwright for frontend testing
- **Service Testing**: Full service startup with mocked external dependencies
- **IoT Simulation**: Fake device data for complete pipeline testing
- **Goal**: Simulate real-world conditions and catch integration bugs

## Test Data Management

### Fixture Organization

- One fixture class per test suite
- Fixtures organized in dedicated `Fixtures` namespace
- Global C# using statements for clean test files
- Naming convention: Match fixture class to test suite name

### Realistic Test Data

- Enforce domain constraints (e.g., energy values always increase)
- Generate time-series data respecting Croatian energy regulations

### Time-Sensitive Testing

- Specific test cases for DST transitions (both directions)
- Quarter-hourly interval aggregation testing
- Team review required for all time-related code changes

## Performance Testing

### Write Performance Priority

- Focus on measurement ingestion performance
- "Heavy writes, light reads" philosophy
- Simple thresholds (e.g., "operation completes in < X seconds")
- Automated performance regression detection in CI

### Database Integration

- Test write performance for TimescaleDB operations
- Verify aggregation performance (second → quarter-hour → daily → monthly)
- Monitor for severe performance degradation

## Critical Testing Areas

### Regulatory Compliance

- Croatian energy law compliance (quarter-hourly intervals)
- HEP measurement standards
- Legal reporting requirements

### Financial Impact

- Catalogue value handling
- Billing calculations
- Invoice generation
- Peak power calculations

### Data Integrity

- Measurement validation pipeline
- Cumulative register handling
- Time zone handling (Croatian time → UTC)

## Testing Workflow

### Development Process

1. Write tests for critical financial logic using property-based approach
2. Implement integration tests for data operations
3. Add E2E tests for complete user workflows
4. Run full test suite on every PR

### Code Review Requirements

- All time-related changes require team review
- Critical financial calculations need thorough test coverage
- Performance tests must pass before merge

### Error Handling Philosophy

- Critical functions expect clean, validated input data
- Validation happens at data ingestion, not in calculation functions
- Focus testing on happy path with realistic edge cases

## Tools and Libraries

### Test Data Generation

- **AutoFixture**: Random object creation with constraints
- **Bogus**: Realistic fake data generation

### Test Execution

- **Unit Tests**: Standard .NET testing framework
- **Integration Tests**: Database testing with TimescaleDB
- **E2E Tests**: Playwright for browser automation

### Performance Monitoring

- CI-based performance regression testing
- Azure PostgreSQL monitoring and alerting
- Storage usage alerts (80% threshold)

## Best Practices

### Test Maintenance

- Keep test code as maintainable as production code
- Update tests when requirements change (thoroughness over speed)

### Critical Code Identification

- "Follow the money" - prioritize testing based on financial impact
- Consult with business stakeholders for criticality assessment
- Focus on compliance and billing accuracy

### Domain-Specific Constraints

- Energy measurements always increase (cumulative registers)
- Time intervals must respect Croatian DST rules
- Quarter-hourly aggregation requirements
- Meter certification compliance

## Future Improvements

### Planned Enhancements

- Expand E2E test coverage for IoT data validation
- Add monitoring alerts for write performance degradation
- Refactor calculator tests to use fixture pattern

### Monitoring Gaps

- Production performance monitoring for measurement ingestion
- Automated alerts for calculation accuracy issues
- Better test coverage for regulatory edge cases

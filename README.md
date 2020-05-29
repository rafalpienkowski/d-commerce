# dCommerce

## Introduction

This repository contains a solution for an eCommerce business called `dCommerce`. The uniqueness of the system lies in its distribution. 

This project aims to show the benefits and drawbacks of a distributed system.

## Background

Let's take a look at the organization chart of the company.

![Organization chart](./docs/org_chart.png)

According to [the Conway's Law](https://en.wikipedia.org/wiki/Conway%27s_law):

> "Any organization that designs a system (defined broadly) will produce a design whose structure is a copy of the organization's communication structure."

This statement has a reflection in the development teams structure which presents the picture below:

![Dev teams](./docs/dev_teams.png)

Each department has it's own development team which builds part of the system. To ensure the non-functional requirements like scalability, reliability and last but not least, parallel development, the solution architect decided to design `a distributed system`.

For a better understanding of what happens when a customer place an order, below is the flow chart with communication between context details.

![Order flow](./docs/order_flow.png)

## Patterns used in project

- Events
- Commands
- Retry policies
- Outbox pattern

## Technologies

This project uses technologies and frameworks:
- .Net Core 3.1
- [Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-3.1)
- [Entity Framework](https://docs.microsoft.com/en-us/ef/)
- [MassTransit](https://masstransit-project.com/)
- [docker](https://www.docker.com/)
- [PostgreSQL](https://www.postgresql.org/)
- [RabbitMQ](https://www.rabbitmq.com/)
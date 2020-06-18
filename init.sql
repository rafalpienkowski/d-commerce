CREATE SCHEMA IF NOT exists web;
CREATE TABLE IF NOT EXISTS web."Orders" (
	"Id" uuid NOT NULL,
	"Number" varchar(15) NULL,
	"UserId" varchar(40) NULL,
	"Status" varchar(20) NOT NULL,
	"LastUpdate" timestamp NOT NULL,
	CONSTRAINT orders_pkey PRIMARY KEY ("Id")
);

CREATE SCHEMA IF NOT exists sales;
CREATE TABLE IF NOT EXISTS sales."Orders" (
	"Id" uuid NOT NULL,
	"Number" varchar(15) NOT NULL,
	"UserId" varchar(40) NOT NULL,
	"ShippingTypeId" varchar(15) NOT NULL,
	"ProductIds" varchar(150) NOT NULL,
	"TimeStamp" timestamp NOT NULL,
	"Amount" decimal(10,2) NOT NULL,
	CONSTRAINT orders_pkey PRIMARY KEY ("Id")
);
CREATE TABLE IF NOT EXISTS sales."OrdersPlaced" (
	"Id" uuid NOT NULL,
	"Number" varchar(15) NULL,
	"UserId" varchar(40) NULL,
	CONSTRAINT orders_placed_pkey PRIMARY KEY ("Id")
);
CREATE INDEX IF NOT EXISTS orders_placed_idx ON sales."OrdersPlaced"("UserId");


CREATE SCHEMA IF NOT exists billing;
CREATE TABLE IF NOT EXISTS billing."PaymentDetailses" (
	"OrderId" uuid NOT NULL,
	"CardNumber" varchar(40) NOT NULL,
	"Status" int4 NOT NULL,
	"TimeStamp" timestamp NOT NULL,
	"Amount" decimal(10,2) NOT NULL,
	CONSTRAINT payment_detailses_pkey PRIMARY KEY ("OrderId")
);

CREATE SCHEMA IF NOT exists shipping;
CREATE TABLE IF NOT EXISTS shipping."ShippingOrders" (
	"OrderId" uuid NOT NULL,
	"UserId" varchar(40) NOT NULL,
	"ShippingTypeId" varchar(15) NOT NULL,
	"Status" int4 NOT NULL,
	CONSTRAINT shipping_orders_pkey PRIMARY KEY ("OrderId")
);

CREATE TABLE IF NOT EXISTS shipping."PersistedEvents" (
	"Id" uuid NOT NULL,
	"TimeStamp" timestamp NOT NULL,
	"Type" varchar(150) NOT NULL,
	"Body" varchar(4000) NOT NULL,
	"Processed" bool NOT NULL DEFAULT false,
	CONSTRAINT persisted_events_pkey PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS shipping."EventContexts" (
	"EventId" uuid NOT NULL,
	"TraceId" varchar(40) NOT NULL,
	"SpanId" varchar(40) NOT NULL,
	CONSTRAINT event_contexts_pkey PRIMARY KEY ("EventId")
);
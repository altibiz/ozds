--
-- PostgreSQL database dump
--

-- Dumped from database version 14.7 (Ubuntu 14.7-1.pgdg22.04+1)
-- Dumped by pg_dump version 14.7 (Ubuntu 14.7-1.pgdg22.04+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Document; Type: TABLE; Schema: public; Owner: ozds
--

CREATE TABLE public."Document" (
    "Id" bigint NOT NULL,
    "Type" character varying(255) NOT NULL,
    "Content" text,
    "Version" bigint DEFAULT 0 NOT NULL
);


ALTER TABLE public."Document" OWNER TO ozds;

--
-- Name: Identifiers; Type: TABLE; Schema: public; Owner: ozds
--

CREATE TABLE public."Identifiers" (
    dimension character varying(255) NOT NULL,
    nextval bigint
);


ALTER TABLE public."Identifiers" OWNER TO ozds;

--
-- Name: UserByClaimIndex; Type: TABLE; Schema: public; Owner: ozds
--

CREATE TABLE public."UserByClaimIndex" (
    "Id" integer NOT NULL,
    "DocumentId" bigint,
    "ClaimType" character varying(255),
    "ClaimValue" character varying(255)
);


ALTER TABLE public."UserByClaimIndex" OWNER TO ozds;

--
-- Name: UserByClaimIndex_Id_seq; Type: SEQUENCE; Schema: public; Owner: ozds
--

CREATE SEQUENCE public."UserByClaimIndex_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."UserByClaimIndex_Id_seq" OWNER TO ozds;

--
-- Name: UserByClaimIndex_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: ozds
--

ALTER SEQUENCE public."UserByClaimIndex_Id_seq" OWNED BY public."UserByClaimIndex"."Id";


--
-- Name: UserByLoginInfoIndex; Type: TABLE; Schema: public; Owner: ozds
--

CREATE TABLE public."UserByLoginInfoIndex" (
    "Id" integer NOT NULL,
    "DocumentId" bigint,
    "LoginProvider" character varying(255),
    "ProviderKey" character varying(255)
);


ALTER TABLE public."UserByLoginInfoIndex" OWNER TO ozds;

--
-- Name: UserByLoginInfoIndex_Id_seq; Type: SEQUENCE; Schema: public; Owner: ozds
--

CREATE SEQUENCE public."UserByLoginInfoIndex_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."UserByLoginInfoIndex_Id_seq" OWNER TO ozds;

--
-- Name: UserByLoginInfoIndex_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: ozds
--

ALTER SEQUENCE public."UserByLoginInfoIndex_Id_seq" OWNED BY public."UserByLoginInfoIndex"."Id";


--
-- Name: UserByRoleNameIndex; Type: TABLE; Schema: public; Owner: ozds
--

CREATE TABLE public."UserByRoleNameIndex" (
    "Id" integer NOT NULL,
    "RoleName" character varying(255),
    "Count" integer
);


ALTER TABLE public."UserByRoleNameIndex" OWNER TO ozds;

--
-- Name: UserByRoleNameIndex_Document; Type: TABLE; Schema: public; Owner: ozds
--

CREATE TABLE public."UserByRoleNameIndex_Document" (
    "UserByRoleNameIndexId" bigint NOT NULL,
    "DocumentId" bigint NOT NULL
);


ALTER TABLE public."UserByRoleNameIndex_Document" OWNER TO ozds;

--
-- Name: UserByRoleNameIndex_Id_seq; Type: SEQUENCE; Schema: public; Owner: ozds
--

CREATE SEQUENCE public."UserByRoleNameIndex_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."UserByRoleNameIndex_Id_seq" OWNER TO ozds;

--
-- Name: UserByRoleNameIndex_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: ozds
--

ALTER SEQUENCE public."UserByRoleNameIndex_Id_seq" OWNED BY public."UserByRoleNameIndex"."Id";


--
-- Name: UserIndex; Type: TABLE; Schema: public; Owner: ozds
--

CREATE TABLE public."UserIndex" (
    "Id" integer NOT NULL,
    "DocumentId" bigint,
    "NormalizedUserName" character varying(255),
    "NormalizedEmail" character varying(255),
    "IsEnabled" boolean DEFAULT true NOT NULL,
    "IsLockoutEnabled" boolean DEFAULT false NOT NULL,
    "LockoutEndUtc" timestamp without time zone,
    "AccessFailedCount" integer DEFAULT 0 NOT NULL,
    "UserId" character varying(255)
);


ALTER TABLE public."UserIndex" OWNER TO ozds;

--
-- Name: UserIndex_Id_seq; Type: SEQUENCE; Schema: public; Owner: ozds
--

CREATE SEQUENCE public."UserIndex_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."UserIndex_Id_seq" OWNER TO ozds;

--
-- Name: UserIndex_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: ozds
--

ALTER SEQUENCE public."UserIndex_Id_seq" OWNED BY public."UserIndex"."Id";


--
-- Name: UserByClaimIndex Id; Type: DEFAULT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserByClaimIndex" ALTER COLUMN "Id" SET DEFAULT nextval('public."UserByClaimIndex_Id_seq"'::regclass);


--
-- Name: UserByLoginInfoIndex Id; Type: DEFAULT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserByLoginInfoIndex" ALTER COLUMN "Id" SET DEFAULT nextval('public."UserByLoginInfoIndex_Id_seq"'::regclass);


--
-- Name: UserByRoleNameIndex Id; Type: DEFAULT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserByRoleNameIndex" ALTER COLUMN "Id" SET DEFAULT nextval('public."UserByRoleNameIndex_Id_seq"'::regclass);


--
-- Name: UserIndex Id; Type: DEFAULT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserIndex" ALTER COLUMN "Id" SET DEFAULT nextval('public."UserIndex_Id_seq"'::regclass);


--
-- Data for Name: Document; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public."Document" ("Id", "Type", "Content", "Version") FROM stdin;
1	OrchardCore.Environment.Shell.Descriptor.Models.ShellDescriptor, OrchardCore.Abstractions	{"SerialNumber":2,"Features":[{"Id":"Ozds.Server"},{"Id":"Application.Default"},{"Id":"OrchardCore.Settings"},{"Id":"OrchardCore.Scripting"},{"Id":"OrchardCore.Recipes"},{"Id":"OrchardCore.Resources"},{"Id":"OrchardCore.Features"},{"Id":"OrchardCore.Admin"},{"Id":"OrchardCore.Roles.Core"},{"Id":"OrchardCore.Users"},{"Id":"OrchardCore.Navigation"},{"Id":"OrchardCore.Roles"},{"Id":"OrchardCore.Themes"},{"Id":"SafeMode"},{"Id":"TheAdmin"}],"Installed":[{"SerialNumber":1,"Id":"Ozds.Server"},{"SerialNumber":1,"Id":"Application.Default"},{"SerialNumber":1,"Id":"OrchardCore.Features"},{"SerialNumber":1,"Id":"OrchardCore.Scripting"},{"SerialNumber":1,"Id":"OrchardCore.Recipes"},{"SerialNumber":2,"Id":"OrchardCore.Settings"},{"SerialNumber":2,"Id":"OrchardCore.Resources"},{"SerialNumber":2,"Id":"OrchardCore.Admin"},{"SerialNumber":2,"Id":"OrchardCore.Roles.Core"},{"SerialNumber":2,"Id":"OrchardCore.Users"},{"SerialNumber":2,"Id":"OrchardCore.Navigation"},{"SerialNumber":2,"Id":"OrchardCore.Roles"},{"SerialNumber":2,"Id":"OrchardCore.Themes"},{"SerialNumber":2,"Id":"SafeMode"},{"SerialNumber":2,"Id":"TheAdmin"}]}	1
21	OrchardCore.Data.Migration.Records.DataMigrationRecord, OrchardCore.Data	{"Id":21,"DataMigrations":[{"DataMigrationClass":"OrchardCore.Users.Migrations","Version":13}]}	1
41	OrchardCore.Settings.SiteSettings, OrchardCore.Settings	{"MaxPagedCount":0,"MaxPageSize":100,"PageSize":10,"TimeZoneId":"Europe/Zagreb","ResourceDebugMode":0,"SiteName":"OZDS","SiteSalt":"e618937e612f4fe99ff5b0dfe02e7e56","PageTitleFormat":"{% page_title Site.SiteName, position: \\"after\\", separator: \\" - \\" %}","SuperUser":"49ctnrj0sq4g1rbdv6chekw2cq","UseCdn":false,"HomeRoute":{},"AppendVersion":true,"CacheMode":0,"Properties":{"CurrentThemeName":"SafeMode","CurrentAdminThemeName":"TheAdmin"},"Identifier":"4hysn5vdmrsf9tjkszs92a6cw9"}	2
42	OrchardCore.Users.Models.User, OrchardCore.Users.Core	{"Id":42,"UserId":"49ctnrj0sq4g1rbdv6chekw2cq","UserName":"super","NormalizedUserName":"SUPER","Email":"super@dev.com","NormalizedEmail":"SUPER@DEV.COM","PasswordHash":"AQAAAAIAAYagAAAAEK23dnWxKQaQGmq9aku8xZlkGoXf1o5avKZNNDpgc1LyJLcNlaJ1RVi/GRn4CUg2ow==","SecurityStamp":"IRGRVT6NBEXMFUVMM3FMUK6MLCSBQMDA","EmailConfirmed":true,"PhoneNumberConfirmed":false,"IsEnabled":true,"TwoFactorEnabled":false,"IsLockoutEnabled":true,"AccessFailedCount":0,"RoleNames":["Administrator"],"UserClaims":[],"LoginInfos":[],"UserTokens":[],"Properties":{}}	1
61	OrchardCore.Users.Models.User, OrchardCore.Users.Core	{"Id":61,"UserId":"41qe2wwv2xxf7wkzj43mfb9sns","UserName":"network-user","NormalizedUserName":"NETWORK-USER","Email":"network-user@dev.com","PhoneNumber":"+385951231234","NormalizedEmail":"NETWORK-USER@DEV.COM","PasswordHash":"AQAAAAIAAYagAAAAEKelXMm720JATREjpHLQnfhCIoB60NPE1hVwZtDYQ7BaGgs0Jeir3KyC6JXPMDWiRw==","SecurityStamp":"PR7HNCEGYHG3RXWAAMDGSJ4CL7Y64ODK","EmailConfirmed":false,"PhoneNumberConfirmed":false,"IsEnabled":true,"TwoFactorEnabled":false,"IsLockoutEnabled":true,"AccessFailedCount":0,"RoleNames":[],"UserClaims":[],"LoginInfos":[],"UserTokens":[],"Properties":{}}	1
62	OrchardCore.Users.Models.User, OrchardCore.Users.Core	{"Id":62,"UserId":"4pm12mpw0cddkrb7xp4dcg25d0","UserName":"location","NormalizedUserName":"LOCATION","Email":"location@dev.com","PhoneNumber":"+385951231234","NormalizedEmail":"LOCATION@DEV.COM","PasswordHash":"AQAAAAIAAYagAAAAEGw7SGCcJ4qYniDm1QUl12S64zcBtm4u1rMowuGgNr3nHFQW6PCB/yzT4o2zFTFGUQ==","SecurityStamp":"FRNAIBLKXK2LLAL6AYOIJSKN5RX45PUB","EmailConfirmed":false,"PhoneNumberConfirmed":false,"IsEnabled":true,"TwoFactorEnabled":false,"IsLockoutEnabled":true,"AccessFailedCount":0,"RoleNames":[],"UserClaims":[],"LoginInfos":[],"UserTokens":[],"Properties":{}}	1
81	OrchardCore.Users.Models.User, OrchardCore.Users.Core	{"Id":81,"UserId":"4kx6mcy0n9cgf7ff6bt2aear5d","UserName":"operator","NormalizedUserName":"OPERATOR","Email":"operator@dev.com","PhoneNumber":"+385951111111","NormalizedEmail":"OPERATOR@DEV.COM","PasswordHash":"AQAAAAIAAYagAAAAEA9/jHTakJKWm2PsLgKbO1dsPuLqbWOGjbwWtdfEd67Zj2WslI+3apSsym3k5TPOEA==","SecurityStamp":"ZE4CTM4P5UADB5JMJ5YUG7RJRCSISHF5","EmailConfirmed":false,"PhoneNumberConfirmed":false,"IsEnabled":true,"TwoFactorEnabled":false,"IsLockoutEnabled":true,"AccessFailedCount":0,"RoleNames":[],"UserClaims":[],"LoginInfos":[],"UserTokens":[],"Properties":{}}	1
\.


--
-- Data for Name: Identifiers; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public."Identifiers" (dimension, nextval) FROM stdin;
	101
\.


--
-- Data for Name: UserByClaimIndex; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public."UserByClaimIndex" ("Id", "DocumentId", "ClaimType", "ClaimValue") FROM stdin;
\.


--
-- Data for Name: UserByLoginInfoIndex; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public."UserByLoginInfoIndex" ("Id", "DocumentId", "LoginProvider", "ProviderKey") FROM stdin;
\.


--
-- Data for Name: UserByRoleNameIndex; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public."UserByRoleNameIndex" ("Id", "RoleName", "Count") FROM stdin;
1	ADMINISTRATOR	1
2	AUTHENTICATED	3
\.


--
-- Data for Name: UserByRoleNameIndex_Document; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public."UserByRoleNameIndex_Document" ("UserByRoleNameIndexId", "DocumentId") FROM stdin;
1	42
1	42
2	61
2	62
2	81
\.


--
-- Data for Name: UserIndex; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public."UserIndex" ("Id", "DocumentId", "NormalizedUserName", "NormalizedEmail", "IsEnabled", "IsLockoutEnabled", "LockoutEndUtc", "AccessFailedCount", "UserId") FROM stdin;
1	42	SUPER	SUPER@DEV.COM	t	t	\N	0	49ctnrj0sq4g1rbdv6chekw2cq
2	61	NETWORK-USER	NETWORK-USER@DEV.COM	t	t	\N	0	41qe2wwv2xxf7wkzj43mfb9sns
3	62	LOCATION	LOCATION@DEV.COM	t	t	\N	0	4pm12mpw0cddkrb7xp4dcg25d0
4	81	OPERATOR	OPERATOR@DEV.COM	t	t	\N	0	4kx6mcy0n9cgf7ff6bt2aear5d
\.


--
-- Name: UserByClaimIndex_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public."UserByClaimIndex_Id_seq"', 1, false);


--
-- Name: UserByLoginInfoIndex_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public."UserByLoginInfoIndex_Id_seq"', 1, false);


--
-- Name: UserByRoleNameIndex_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public."UserByRoleNameIndex_Id_seq"', 2, true);


--
-- Name: UserIndex_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public."UserIndex_Id_seq"', 4, true);


--
-- Name: Document Document_pkey; Type: CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."Document"
    ADD CONSTRAINT "Document_pkey" PRIMARY KEY ("Id");


--
-- Name: Identifiers Identifiers_pkey; Type: CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."Identifiers"
    ADD CONSTRAINT "Identifiers_pkey" PRIMARY KEY (dimension);


--
-- Name: UserByClaimIndex UserByClaimIndex_pkey; Type: CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserByClaimIndex"
    ADD CONSTRAINT "UserByClaimIndex_pkey" PRIMARY KEY ("Id");


--
-- Name: UserByLoginInfoIndex UserByLoginInfoIndex_pkey; Type: CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserByLoginInfoIndex"
    ADD CONSTRAINT "UserByLoginInfoIndex_pkey" PRIMARY KEY ("Id");


--
-- Name: UserByRoleNameIndex UserByRoleNameIndex_pkey; Type: CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserByRoleNameIndex"
    ADD CONSTRAINT "UserByRoleNameIndex_pkey" PRIMARY KEY ("Id");


--
-- Name: UserIndex UserIndex_pkey; Type: CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserIndex"
    ADD CONSTRAINT "UserIndex_pkey" PRIMARY KEY ("Id");


--
-- Name: IDX_FK_UserByClaimIndex; Type: INDEX; Schema: public; Owner: ozds
--

CREATE INDEX "IDX_FK_UserByClaimIndex" ON public."UserByClaimIndex" USING btree ("DocumentId");


--
-- Name: IDX_FK_UserByLoginInfoIndex; Type: INDEX; Schema: public; Owner: ozds
--

CREATE INDEX "IDX_FK_UserByLoginInfoIndex" ON public."UserByLoginInfoIndex" USING btree ("DocumentId");


--
-- Name: IDX_FK_UserByRoleNameIndex_Document; Type: INDEX; Schema: public; Owner: ozds
--

CREATE INDEX "IDX_FK_UserByRoleNameIndex_Document" ON public."UserByRoleNameIndex_Document" USING btree ("UserByRoleNameIndexId", "DocumentId");


--
-- Name: IDX_FK_UserIndex; Type: INDEX; Schema: public; Owner: ozds
--

CREATE INDEX "IDX_FK_UserIndex" ON public."UserIndex" USING btree ("DocumentId");


--
-- Name: IDX_UserByClaimIndex_DocumentId; Type: INDEX; Schema: public; Owner: ozds
--

CREATE INDEX "IDX_UserByClaimIndex_DocumentId" ON public."UserByClaimIndex" USING btree ("DocumentId", "ClaimType", "ClaimValue");


--
-- Name: IDX_UserByLoginInfoIndex_DocumentId; Type: INDEX; Schema: public; Owner: ozds
--

CREATE INDEX "IDX_UserByLoginInfoIndex_DocumentId" ON public."UserByLoginInfoIndex" USING btree ("DocumentId", "LoginProvider", "ProviderKey");


--
-- Name: IDX_UserByRoleNameIndex_RoleName; Type: INDEX; Schema: public; Owner: ozds
--

CREATE INDEX "IDX_UserByRoleNameIndex_RoleName" ON public."UserByRoleNameIndex" USING btree ("RoleName");


--
-- Name: IDX_UserIndex_DocumentId; Type: INDEX; Schema: public; Owner: ozds
--

CREATE INDEX "IDX_UserIndex_DocumentId" ON public."UserIndex" USING btree ("DocumentId", "UserId", "NormalizedUserName", "NormalizedEmail", "IsEnabled");


--
-- Name: IDX_UserIndex_Lockout; Type: INDEX; Schema: public; Owner: ozds
--

CREATE INDEX "IDX_UserIndex_Lockout" ON public."UserIndex" USING btree ("DocumentId", "IsLockoutEnabled", "LockoutEndUtc", "AccessFailedCount");


--
-- Name: IX_Document_Type; Type: INDEX; Schema: public; Owner: ozds
--

CREATE INDEX "IX_Document_Type" ON public."Document" USING btree ("Type");


--
-- Name: UserByClaimIndex fk_userbyclaimindex; Type: FK CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserByClaimIndex"
    ADD CONSTRAINT fk_userbyclaimindex FOREIGN KEY ("DocumentId") REFERENCES public."Document"("Id");


--
-- Name: UserByLoginInfoIndex fk_userbylogininfoindex; Type: FK CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserByLoginInfoIndex"
    ADD CONSTRAINT fk_userbylogininfoindex FOREIGN KEY ("DocumentId") REFERENCES public."Document"("Id");


--
-- Name: UserByRoleNameIndex_Document fk_userbyrolenameindex_document_documentid; Type: FK CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserByRoleNameIndex_Document"
    ADD CONSTRAINT fk_userbyrolenameindex_document_documentid FOREIGN KEY ("DocumentId") REFERENCES public."Document"("Id");


--
-- Name: UserByRoleNameIndex_Document fk_userbyrolenameindex_document_id; Type: FK CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserByRoleNameIndex_Document"
    ADD CONSTRAINT fk_userbyrolenameindex_document_id FOREIGN KEY ("UserByRoleNameIndexId") REFERENCES public."UserByRoleNameIndex"("Id");


--
-- Name: UserIndex fk_userindex; Type: FK CONSTRAINT; Schema: public; Owner: ozds
--

ALTER TABLE ONLY public."UserIndex"
    ADD CONSTRAINT fk_userindex FOREIGN KEY ("DocumentId") REFERENCES public."Document"("Id");


--
-- PostgreSQL database dump complete
--


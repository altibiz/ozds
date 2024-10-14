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

--
-- Data for Name: representatives; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.representatives (id, physical_person_name, physical_person_email, physical_person_phone_number, title, created_on, created_by_id, last_updated_on, last_updated_by_id, is_deleted, deleted_on, deleted_by_id, topics, role) FROM stdin;
41qe2wwv2xxf7wkzj43mfb9sns	network-user	network-user@dev.com	111111111	network-user	2024-04-20 13:20:37.385397+00	\N	\N	\N	f	\N	\N	{}	network_user_representative
4pm12mpw0cddkrb7xp4dcg25d0	location	location@dev.com	111111111	location	2024-04-20 13:20:55.979492+00	\N	2024-04-24 19:45:09.738243+00	\N	f	\N	\N	{}	location_representative
49ctnrj0sq4g1rbdv6chekw2cq	super	super@dev.com	111111111	super	2024-04-20 11:15:13.67325+00	\N	\N	\N	f	\N	\N	{}	network_user_representative
4kx6mcy0n9cgf7ff6bt2aear5d	operator	operator@dev.com	111111111	operator	2024-04-22 16:27:41.31809+00	\N	2024-09-23 22:40:30.757071+00	\N	f	\N	\N	{messenger,all,messenger_inactivity,invalid_push,error}	operator_representative
\.


--
-- Data for Name: network_user_catalogues; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.network_user_catalogues (id, kind, active_energy_total_import_t0_price_eur, reactive_energy_total_ramped_t0_price_eur, meter_fee_price_eur, active_energy_total_import_t1_price_eur, active_energy_total_import_t2_price_eur, active_power_total_import_t1_price_eur, created_on, created_by_id, last_updated_on, last_updated_by_id, is_deleted, deleted_on, deleted_by_id, title) FROM stdin;
4	WhiteMediumNetworkUserCatalogueEntity	\N	0	0	0	0	0	2024-04-19 13:38:02.761855+00	\N	\N	\N	f	\N	\N	test
1	RedLowNetworkUserCatalogueEntity	\N	0.021236	5.481	0.029199	0.013272	5.176	2024-04-19 13:37:35.162942+00	\N	2024-04-29 12:44:31.85688+00	\N	f	\N	\N	test
2	BlueLowNetworkUserCatalogueEntity	0.041144	0.021236	5.481	\N	\N	\N	2024-04-19 13:37:43.769977+00	\N	2024-04-29 12:45:42.845717+00	\N	f	\N	\N	test
3	WhiteLowNetworkUserCatalogueEntity	\N	0.021236	5.481	0.051762	0.022563	\N	2024-04-19 13:37:53.340328+00	\N	2024-04-29 12:45:10.849424+00	\N	f	\N	\N	test
5	WhiteMediumNetworkUserCatalogueEntity	\N	0.021236	8.760	0.018581	0.00929	3.451	2024-04-29 12:43:32.154907+00	\N	\N	\N	f	\N	\N	test
\.


--
-- Data for Name: regulatory_catalogues; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.regulatory_catalogues (id, active_energy_total_import_t1_price_eur, active_energy_total_import_t2_price_eur, renewable_energy_fee_price_eur, business_usage_fee_price_eur, tax_rate_percent, title, created_on, created_by_id, last_updated_on, last_updated_by_id, is_deleted, deleted_on, deleted_by_id) FROM stdin;
1	0.1	0.06	0.013936	0.0005	13	test	2024-04-19 13:37:25.757982+00	\N	2024-04-29 13:11:14.047365+00	\N	f	\N	\N
\.


--
-- Data for Name: locations; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.locations (id, white_medium_catalogue_id, blue_low_catalogue_id, white_low_catalogue_id, red_low_catalogue_id, regulatory_catalogue_id, title, created_on, created_by_id, last_updated_on, last_updated_by_id, is_deleted, deleted_on, deleted_by_id, legal_person_address, legal_person_city, legal_person_email, legal_person_name, legal_person_phone_number, legal_person_postal_code, legal_person_social_security_number, alti_biz_sub_project_code) FROM stdin;
1	4	2	3	1	1	KINGCROSS	2024-04-20 11:06:03.068193+00	\N	2024-05-02 21:02:44.255836+00	\N	f	\N	\N	test	test	test@dev.com	test	111111111	11111	11111111111	8045-1/21
\.


--
-- Data for Name: messengers; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.messengers (id, location_id, title, created_on, created_by_id, last_updated_on, last_updated_by_id, is_deleted, deleted_on, deleted_by_id, kind, max_inactivity_period_duration, max_inactivity_period_multiplier, push_delay_period_duration, push_delay_period_multiplier) FROM stdin;
pidgeon	1	test	2024-04-20 11:06:29.979846+00	\N	2024-09-23 22:40:13.621239+00	\N	f	\N	\N	PidgeonMessengerEntity	second	15	second	0
\.


--
-- Data for Name: events; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.events (id, title, "timestamp", kind, auditable_entity_id, auditable_entity_type, auditable_entity_table, representative_id, messenger_id, content, level, audit, categories) FROM stdin;
6	Created LocationEntity test	2024-04-20 11:06:03.068193+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
1	Created RegulatoryCatalogueEntity test	2024-04-19 13:37:25.757982+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
2	Created RedLowNetworkUserCatalogueEntity test	2024-04-19 13:37:35.162942+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
3	Created BlueLowNetworkUserCatalogueEntity test	2024-04-19 13:37:43.769977+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
4	Created WhiteLowNetworkUserCatalogueEntity test	2024-04-19 13:37:53.340328+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
5	Created WhiteMediumNetworkUserCatalogueEntity test	2024-04-19 13:38:02.761855+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
7	Created MessengerEntity test	2024-04-20 11:06:29.979846+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
8	Created RepresentativeEntity super	2024-04-20 11:15:13.67325+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
9	Created NetworkUserEntity test	2024-04-20 11:16:00.724216+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
10	Created AbbB2xMeasurementValidatorEntity test	2024-04-20 11:16:52.315739+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
11	Created SchneideriEM3xxxMeasurementValidatorEntity test	2024-04-20 11:17:27.434718+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
12	Created AbbB2xMeterEntity test	2024-04-20 11:21:08.1314+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
13	Created SchneideriEM3xxxMeterEntity test	2024-04-20 11:21:41.28222+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
14	Created RepresentativeEntity network-user	2024-04-20 13:20:37.385397+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
15	Created RepresentativeEntity location	2024-04-20 13:20:55.979492+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
16	Created RepresentativeEntity operator	2024-04-22 16:27:41.31809+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
17	Updated LocationEntityProxy KINGCROSS	2024-04-22 16:42:28.304675+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
18	Updated NetworkUserEntityProxy DM	2024-04-22 16:42:56.269108+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
19	Updated AbbB2xMeterEntityProxy Abb	2024-04-22 16:43:24.823248+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
20	Updated SchneideriEM3xxxMeterEntityProxy Schnider	2024-04-22 16:43:36.740555+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
21	Created NetworkUserMeasurementLocationEntity AbbOMM	2024-04-22 16:45:20.621524+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
22	Created NetworkUserMeasurementLocationEntity SchniderOMM	2024-04-22 16:45:48.280561+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
23	Updated RepresentativeEntityProxy operator	2024-04-24 19:40:02.587762+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
24	Updated RepresentativeEntityProxy location	2024-04-24 19:45:09.738243+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
25	Updated NetworkUserEntityProxy DM	2024-04-29 12:37:55.131879+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
26	Created WhiteMediumNetworkUserCatalogueEntity test	2024-04-29 12:43:32.154907+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	creation	{all,audit}
27	Updated RedLowNetworkUserCatalogueEntityProxy test	2024-04-29 12:44:31.85688+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
28	Updated WhiteLowNetworkUserCatalogueEntityProxy test	2024-04-29 12:45:10.849424+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
30	Updated RegulatoryCatalogueEntityProxy test	2024-04-29 13:11:14.047365+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
29	Updated BlueLowNetworkUserCatalogueEntityProxy test	2024-04-29 12:45:42.845717+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
31	Updated LocationEntityProxy KINGCROSS	2024-05-02 20:58:44.656211+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
32	Updated LocationEntityProxy KINGCROSS	2024-05-02 21:02:44.255836+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{}	debug	modification	{all,audit}
33	Startup	2024-09-23 13:28:21.204385+00	SystemEventEntity	\N	\N	\N	\N	\N	{"Message": "Startup"}	info	\N	{all,lifecycle}
34	Updated PidgeonMessengerEntity test	2024-09-23 13:30:37.884474+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{"Type": "Modified", "Properties": [{"Name": "CreatedOn", "NewValue": "20. 04. 2024. 11:06:29 +00:00", "OldValue": "20. 04. 2024. 11:06:29 +00:00"}, {"Name": "IsDeleted", "NewValue": "False", "OldValue": "False"}, {"Name": "LastUpdatedOn", "NewValue": "23. 09. 2024. 13:30:37 +00:00", "OldValue": null}, {"Name": "_locationId", "NewValue": "1", "OldValue": "1"}]}	debug	modification	{all,audit}
35	Startup	2024-09-23 22:39:55.37698+00	SystemEventEntity	\N	\N	\N	\N	\N	{"Message": "Startup"}	info	\N	{all,lifecycle}
36	Updated PidgeonMessengerEntity test	2024-09-23 22:40:13.621239+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{"Type": "Modified", "Properties": [{"Name": "CreatedOn", "NewValue": "20. 04. 2024. 11:06:29 +00:00", "OldValue": "20. 04. 2024. 11:06:29 +00:00"}, {"Name": "IsDeleted", "NewValue": "False", "OldValue": "False"}, {"Name": "LastUpdatedOn", "NewValue": "23. 09. 2024. 22:40:13 +00:00", "OldValue": "23. 09. 2024. 13:30:37 +00:00"}, {"Name": "_locationId", "NewValue": "1", "OldValue": "1"}]}	debug	modification	{all,audit}
37	Updated RepresentativeEntity operator	2024-09-23 22:40:30.757071+00	SystemAuditEventEntity	\N	\N	\N	\N	\N	{"Type": "Modified", "Properties": [{"Name": "CreatedOn", "NewValue": "22. 04. 2024. 16:27:41 +00:00", "OldValue": "22. 04. 2024. 16:27:41 +00:00"}, {"Name": "IsDeleted", "NewValue": "False", "OldValue": "False"}, {"Name": "LastUpdatedOn", "NewValue": "23. 09. 2024. 22:40:30 +00:00", "OldValue": "24. 04. 2024. 19:40:02 +00:00"}, {"Name": "Role", "NewValue": "OperatorRepresentative", "OldValue": "OperatorRepresentative"}, {"Name": "Topics", "NewValue": "System.Collections.Generic.List`1[Ozds.Data.Entities.Enums.TopicEntity]", "OldValue": "System.Collections.Generic.List`1[Ozds.Data.Entities.Enums.TopicEntity]"}]}	debug	modification	{all,audit}
38	Exception has been thrown by the target of an invocation.	2024-09-23 22:40:34.988241+00	SystemEventEntity	\N	\N	\N	\N	\N	{"Message": "Exception has been thrown by the target of an invocation.", "Exception": "System.Reflection.TargetInvocationException: Exception has been thrown by the target of an invocation.\\n ---> System.ObjectDisposedException: Cannot access a disposed context instance. A common cause of this error is disposing a context instance that was resolved from dependency injection and then later trying to use the same context instance elsewhere in your application. This may occur if you are calling 'Dispose' on the context instance, or wrapping it in a using statement. If you are using dependency injection, you should let the dependency injection container take care of disposing context instances.\\nObject name: 'DataDbContext'.\\n   at Microsoft.EntityFrameworkCore.DbContext.get_DbContextDependencies()\\n   at Microsoft.EntityFrameworkCore.DbContext.Set[TEntity]()\\n   at InvokeStub_DbContext.Set(Object, Object, IntPtr*)\\n   at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)\\n   --- End of inner exception stack trace ---\\n   at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)\\n   at Ozds.Data.Extensions.DbContextQueryableExtensions.GetQueryable(DbContext context, Type type) in /home/haras/src/ozds/src/Ozds.Data/Extensions/DbContextQueryableExtensions.cs:line 18\\n   at Ozds.Business.Queries.Agnostic.OzdsMeasurementQueries.Read[T](IEnumerable`1 whereClauses, DateTimeOffset fromDate, DateTimeOffset toDate, Int32 pageNumber, Int32 pageCount) in /home/haras/src/ozds/src/Ozds.Business/Queries/Agnostic/OzdsMeasurementQueries.cs:line 32\\n   at Ozds.Business.Queries.Agnostic.OzdsMeasurementQueries.ReadAgnostic(IEnumerable`1 whereClauses, DateTimeOffset fromDate, DateTimeOffset toDate, Int32 pageNumber, Int32 pageCount) in /home/haras/src/ozds/src/Ozds.Business/Queries/Agnostic/OzdsMeasurementQueries.cs:line 65\\n   at Ozds.Client.Shared.OperatorGraph.GetValues(DateTimeOffset fromDate, DateTimeOffset toDate, Boolean reRenderChart) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 360\\n   at Ozds.Client.Shared.OperatorGraph.LeftMenuDataItemClicked(DataEnum type) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 325\\n   at Ozds.Client.Shared.OperatorGraph.LeftMenuItemClicked(String source) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 301\\n   at Ozds.Client.Shared.OperatorGraph.OnInitializedAsync() in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 212\\n   at Microsoft.AspNetCore.Components.ComponentBase.RunInitAndSetParametersAsync()\\n   at Microsoft.AspNetCore.Components.RenderTree.Renderer.GetErrorHandledTask(Task taskToHandle, ComponentState owningComponentState)", "StackTrace": "   at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)\\n   at Ozds.Data.Extensions.DbContextQueryableExtensions.GetQueryable(DbContext context, Type type) in /home/haras/src/ozds/src/Ozds.Data/Extensions/DbContextQueryableExtensions.cs:line 18\\n   at Ozds.Business.Queries.Agnostic.OzdsMeasurementQueries.Read[T](IEnumerable`1 whereClauses, DateTimeOffset fromDate, DateTimeOffset toDate, Int32 pageNumber, Int32 pageCount) in /home/haras/src/ozds/src/Ozds.Business/Queries/Agnostic/OzdsMeasurementQueries.cs:line 32\\n   at Ozds.Business.Queries.Agnostic.OzdsMeasurementQueries.ReadAgnostic(IEnumerable`1 whereClauses, DateTimeOffset fromDate, DateTimeOffset toDate, Int32 pageNumber, Int32 pageCount) in /home/haras/src/ozds/src/Ozds.Business/Queries/Agnostic/OzdsMeasurementQueries.cs:line 65\\n   at Ozds.Client.Shared.OperatorGraph.GetValues(DateTimeOffset fromDate, DateTimeOffset toDate, Boolean reRenderChart) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 360\\n   at Ozds.Client.Shared.OperatorGraph.LeftMenuDataItemClicked(DataEnum type) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 325\\n   at Ozds.Client.Shared.OperatorGraph.LeftMenuItemClicked(String source) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 301\\n   at Ozds.Client.Shared.OperatorGraph.OnInitializedAsync() in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 212\\n   at Microsoft.AspNetCore.Components.ComponentBase.RunInitAndSetParametersAsync()\\n   at Microsoft.AspNetCore.Components.RenderTree.Renderer.GetErrorHandledTask(Task taskToHandle, ComponentState owningComponentState)"}	error	\N	{all,error}
39	Cannot access a disposed context instance. A common cause of this error is disposing a context instance that was resolved from dependency injection and then later trying to use the same context instance elsewhere in your application. This may occur if you are calling 'Dispose' on the context instance, or wrapping it in a using statement. If you are using dependency injection, you should let the dependency injection container take care of disposing context instances.\nObject name: 'DataDbContext'.	2024-09-23 22:40:35.050354+00	SystemEventEntity	\N	\N	\N	\N	\N	{"Message": "Cannot access a disposed context instance. A common cause of this error is disposing a context instance that was resolved from dependency injection and then later trying to use the same context instance elsewhere in your application. This may occur if you are calling 'Dispose' on the context instance, or wrapping it in a using statement. If you are using dependency injection, you should let the dependency injection container take care of disposing context instances.\\nObject name: 'DataDbContext'.", "Exception": "System.ObjectDisposedException: Cannot access a disposed context instance. A common cause of this error is disposing a context instance that was resolved from dependency injection and then later trying to use the same context instance elsewhere in your application. This may occur if you are calling 'Dispose' on the context instance, or wrapping it in a using statement. If you are using dependency injection, you should let the dependency injection container take care of disposing context instances.\\nObject name: 'DataDbContext'.\\n   at Microsoft.EntityFrameworkCore.DbContext.get_ContextServices()\\n   at Microsoft.EntityFrameworkCore.DbContext.get_ChangeTracker()\\n   at Microsoft.EntityFrameworkCore.Query.CompiledQueryCacheKeyGenerator.GenerateCacheKeyCore(Expression query, Boolean async)\\n   at Microsoft.EntityFrameworkCore.Query.RelationalCompiledQueryCacheKeyGenerator.GenerateCacheKeyCore(Expression query, Boolean async)\\n   at Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal.NpgsqlCompiledQueryCacheKeyGenerator.GenerateCacheKey(Expression query, Boolean async)\\n   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.ExecuteAsync[TResult](Expression query, CancellationToken cancellationToken)\\n   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryProvider.ExecuteAsync[TResult](Expression expression, CancellationToken cancellationToken)\\n   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetAsyncEnumerator(CancellationToken cancellationToken)\\n   at System.Runtime.CompilerServices.ConfiguredCancelableAsyncEnumerable`1.GetAsyncEnumerator()\\n   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync[TSource](IQueryable`1 source, CancellationToken cancellationToken)\\n   at Ozds.Business.Queries.Agnostic.OzdsMeasurementQueries.Read[T](IEnumerable`1 whereClauses, DateTimeOffset fromDate, DateTimeOffset toDate, Int32 pageNumber, Int32 pageCount) in /home/haras/src/ozds/src/Ozds.Business/Queries/Agnostic/OzdsMeasurementQueries.cs:line 45\\n   at Ozds.Business.Queries.Agnostic.OzdsMeasurementQueries.ReadAgnostic(IEnumerable`1 whereClauses, DateTimeOffset fromDate, DateTimeOffset toDate, Int32 pageNumber, Int32 pageCount) in /home/haras/src/ozds/src/Ozds.Business/Queries/Agnostic/OzdsMeasurementQueries.cs:line 65\\n   at Ozds.Client.Shared.OperatorGraph.GetValues(DateTimeOffset fromDate, DateTimeOffset toDate, Boolean reRenderChart) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 360\\n   at Ozds.Client.Shared.OperatorGraph.LeftMenuDataItemClicked(DataEnum type) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 325\\n   at Ozds.Client.Shared.OperatorGraph.LeftMenuItemClicked(String source) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 301\\n   at Ozds.Client.Shared.OperatorGraph.OnInitializedAsync() in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 212\\n   at Microsoft.AspNetCore.Components.ComponentBase.RunInitAndSetParametersAsync()\\n   at Microsoft.AspNetCore.Components.RenderTree.Renderer.GetErrorHandledTask(Task taskToHandle, ComponentState owningComponentState)", "StackTrace": "   at Microsoft.EntityFrameworkCore.DbContext.get_ContextServices()\\n   at Microsoft.EntityFrameworkCore.DbContext.get_ChangeTracker()\\n   at Microsoft.EntityFrameworkCore.Query.CompiledQueryCacheKeyGenerator.GenerateCacheKeyCore(Expression query, Boolean async)\\n   at Microsoft.EntityFrameworkCore.Query.RelationalCompiledQueryCacheKeyGenerator.GenerateCacheKeyCore(Expression query, Boolean async)\\n   at Npgsql.EntityFrameworkCore.PostgreSQL.Query.Internal.NpgsqlCompiledQueryCacheKeyGenerator.GenerateCacheKey(Expression query, Boolean async)\\n   at Microsoft.EntityFrameworkCore.Query.Internal.QueryCompiler.ExecuteAsync[TResult](Expression query, CancellationToken cancellationToken)\\n   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryProvider.ExecuteAsync[TResult](Expression expression, CancellationToken cancellationToken)\\n   at Microsoft.EntityFrameworkCore.Query.Internal.EntityQueryable`1.GetAsyncEnumerator(CancellationToken cancellationToken)\\n   at System.Runtime.CompilerServices.ConfiguredCancelableAsyncEnumerable`1.GetAsyncEnumerator()\\n   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync[TSource](IQueryable`1 source, CancellationToken cancellationToken)\\n   at Ozds.Business.Queries.Agnostic.OzdsMeasurementQueries.Read[T](IEnumerable`1 whereClauses, DateTimeOffset fromDate, DateTimeOffset toDate, Int32 pageNumber, Int32 pageCount) in /home/haras/src/ozds/src/Ozds.Business/Queries/Agnostic/OzdsMeasurementQueries.cs:line 45\\n   at Ozds.Business.Queries.Agnostic.OzdsMeasurementQueries.ReadAgnostic(IEnumerable`1 whereClauses, DateTimeOffset fromDate, DateTimeOffset toDate, Int32 pageNumber, Int32 pageCount) in /home/haras/src/ozds/src/Ozds.Business/Queries/Agnostic/OzdsMeasurementQueries.cs:line 65\\n   at Ozds.Client.Shared.OperatorGraph.GetValues(DateTimeOffset fromDate, DateTimeOffset toDate, Boolean reRenderChart) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 360\\n   at Ozds.Client.Shared.OperatorGraph.LeftMenuDataItemClicked(DataEnum type) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 325\\n   at Ozds.Client.Shared.OperatorGraph.LeftMenuItemClicked(String source) in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 301\\n   at Ozds.Client.Shared.OperatorGraph.OnInitializedAsync() in /home/haras/src/ozds/src/Ozds.Client/Shared/OperatorGraph.razor:line 212\\n   at Microsoft.AspNetCore.Components.ComponentBase.RunInitAndSetParametersAsync()\\n   at Microsoft.AspNetCore.Components.RenderTree.Renderer.GetErrorHandledTask(Task taskToHandle, ComponentState owningComponentState)"}	error	\N	{all,error}
40	The reader is closed	2024-09-23 22:40:35.062264+00	SystemEventEntity	\N	\N	\N	\N	\N	{"Message": "The reader is closed", "Exception": "System.InvalidOperationException: The reader is closed\\n   at Npgsql.ThrowHelper.ThrowInvalidOperationException(String message)\\n   at Npgsql.NpgsqlDataReader.<CheckClosedOrDisposed>g__Throw|138_0(ReaderState state)\\n   at Npgsql.NpgsqlDataReader.ReadAsync(CancellationToken cancellationToken)\\n   at Microsoft.EntityFrameworkCore.Storage.RelationalDataReader.ReadAsync(CancellationToken cancellationToken)\\n   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.AsyncEnumerator.MoveNextAsync()\\n   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync[TSource](IQueryable`1 source, CancellationToken cancellationToken)\\n   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync[TSource](IQueryable`1 source, CancellationToken cancellationToken)\\n   at Ozds.Business.Queries.OzdsMeterTableQueries.GetNetworkUserInvoicesByRepresentative(RepresentativeModel representative, DateTimeOffset fromDate, DateTimeOffset toDate) in /home/haras/src/ozds/src/Ozds.Business/Queries/OzdsMeterTableQueries.cs:line 114\\n   at Ozds.Client.Shared.InvoicesTable.OnInitializedAsync() in /home/haras/src/ozds/src/Ozds.Client/Shared/InvoicesTable.razor:line 142\\n   at Microsoft.AspNetCore.Components.ComponentBase.RunInitAndSetParametersAsync()\\n   at Microsoft.AspNetCore.Components.RenderTree.Renderer.GetErrorHandledTask(Task taskToHandle, ComponentState owningComponentState)", "StackTrace": "   at Npgsql.ThrowHelper.ThrowInvalidOperationException(String message)\\n   at Npgsql.NpgsqlDataReader.<CheckClosedOrDisposed>g__Throw|138_0(ReaderState state)\\n   at Npgsql.NpgsqlDataReader.ReadAsync(CancellationToken cancellationToken)\\n   at Microsoft.EntityFrameworkCore.Storage.RelationalDataReader.ReadAsync(CancellationToken cancellationToken)\\n   at Microsoft.EntityFrameworkCore.Query.Internal.SingleQueryingEnumerable`1.AsyncEnumerator.MoveNextAsync()\\n   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync[TSource](IQueryable`1 source, CancellationToken cancellationToken)\\n   at Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync[TSource](IQueryable`1 source, CancellationToken cancellationToken)\\n   at Ozds.Business.Queries.OzdsMeterTableQueries.GetNetworkUserInvoicesByRepresentative(RepresentativeModel representative, DateTimeOffset fromDate, DateTimeOffset toDate) in /home/haras/src/ozds/src/Ozds.Business/Queries/OzdsMeterTableQueries.cs:line 114\\n   at Ozds.Client.Shared.InvoicesTable.OnInitializedAsync() in /home/haras/src/ozds/src/Ozds.Client/Shared/InvoicesTable.razor:line 142\\n   at Microsoft.AspNetCore.Components.ComponentBase.RunInitAndSetParametersAsync()\\n   at Microsoft.AspNetCore.Components.RenderTree.Renderer.GetErrorHandledTask(Task taskToHandle, ComponentState owningComponentState)"}	error	\N	{all,error}
\.


--
-- Data for Name: location_invoices; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.location_invoices (id, location_id, title, issued_on, issued_by_id, from_date, to_date, al_blue_low_network_user_catalogue_id, al_created_by_id, al_created_on, al_deleted_by_id, al_deleted_on, al_lp_postal_code, al_is_deleted, al_last_updated_by_id, al_last_updated_on, al_red_low_network_user_catalogue_id, al_regulatory_catalogue_id, al_lp_social_security_number, al_white_low_network_user_catalogue_id, al_white_medium_network_user_catalogue_id, tax_eur, total_eur, total_with_tax_eur, al_lp_address, al_lp_city, al_lp_email, al_lp_name, al_lp_phone_number, al_alti_biz_sub_project_code, tax_rate_percent, al_title) FROM stdin;
\.


--
-- Data for Name: location_representatives; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.location_representatives (location_id, representative_id) FROM stdin;
1	4pm12mpw0cddkrb7xp4dcg25d0
\.


--
-- Data for Name: measurement_validators; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.measurement_validators (id, kind, min_voltage_v, max_voltage_v, min_current_a, max_current_a, min_active_power_w, max_active_power_w, min_reactive_power_var, max_reactive_power_var, min_apparent_power_va, max_apparent_power_va, title, created_on, created_by_id, last_updated_on, last_updated_by_id, is_deleted, deleted_on, deleted_by_id) FROM stdin;
1	AbbB2xMeasurementValidatorEntity	160	300	0	80	-24000	24000	-24000	24000	\N	\N	test	2024-04-20 11:16:52.315739+00	\N	\N	\N	f	\N	\N
2	SchneideriEM3xxxMeasurementValidatorEntity	160	300	0	80	-24000	24000	-24000	24000	-24000	24000	test	2024-04-20 11:17:27.434718+00	\N	\N	\N	f	\N	\N
\.


--
-- Data for Name: meters; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.meters (id, messenger_id, connection_power_w, kind, measurement_validator_id, title, created_on, created_by_id, last_updated_on, last_updated_by_id, is_deleted, deleted_on, deleted_by_id, phases) FROM stdin;
schneider-iEM3xxx-19453067	pidgeon	24000	SchneideriEM3xxxMeterEntity	2	Schnider	2024-04-20 11:21:41.28222+00	\N	2024-04-22 16:43:36.740555+00	\N	f	\N	\N	{l1,l2,l3}
abb-B2x-1856212	pidgeon	24000	AbbB2xMeterEntity	1	abb 2	2024-05-07 09:27:22.813965+00	\N	\N	\N	f	\N	\N	{l1,l2,l3}
abb-B2x-1624226	pidgeon	24000	AbbB2xMeterEntity	1	abb 1	2024-04-20 11:21:08.1314+00	\N	2024-04-22 16:43:24.823248+00	\N	f	\N	\N	{l1,l2,l3}
abb-B2x-1856214	pidgeon	24000	AbbB2xMeterEntity	1	abb 3	2024-05-07 09:27:57.115249+00	\N	\N	\N	f	\N	\N	{l1,l2,l3}
\.


--
-- Data for Name: network_users; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.network_users (id, location_id, title, created_on, created_by_id, last_updated_on, last_updated_by_id, is_deleted, deleted_on, deleted_by_id, legal_person_address, legal_person_city, legal_person_email, legal_person_name, legal_person_phone_number, legal_person_postal_code, legal_person_social_security_number, alti_biz_sub_project_code) FROM stdin;
1	1	DM	2024-04-20 11:16:00.724216+00	\N	2024-04-29 12:37:55.131879+00	\N	f	\N	\N	test	test	test@dev.com	test	111111111	11111	11111111111	8045-1/21
\.


--
-- Data for Name: measurement_locations; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.measurement_locations (id, meter_id, kind, location_id, network_user_id, title, created_on, created_by_id, last_updated_on, last_updated_by_id, is_deleted, deleted_on, deleted_by_id, network_user_catalogue_id) FROM stdin;
1	abb-B2x-1624226	NetworkUserMeasurementLocationEntity	\N	1	AbbOMM	2024-04-22 16:45:20.621524+00	\N	\N	\N	f	\N	\N	1
2	schneider-iEM3xxx-19453067	NetworkUserMeasurementLocationEntity	\N	1	SchniderOMM	2024-04-22 16:45:48.280561+00	\N	\N	\N	f	\N	\N	2
\.


--
-- Data for Name: network_user_invoices; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.network_user_invoices (id, network_user_id, title, issued_on, issued_by_id, from_date, to_date, al_created_by_id, al_created_on, al_deleted_by_id, al_deleted_on, al_is_deleted, al_last_updated_by_id, al_last_updated_on, al_regulatory_catalogue_id, al_blue_low_network_user_catalogue_id, anu_created_by_id, anu_created_on, anu_deleted_by_id, anu_deleted_on, al_red_low_network_user_catalogue_id, anu_is_deleted, anu_last_updated_by_id, anu_last_updated_on, al_white_low_network_user_catalogue_id, al_white_medium_network_user_catalogue_id, tax_eur, total_eur, total_with_tax_eur, al_lp_address, al_lp_city, al_lp_email, al_lp_name, al_lp_phone_number, al_lp_postal_code, al_lp_social_security_number, anu_lp_address, anu_lp_city, anu_lp_email, anu_lp_name, anu_lp_phone_number, anu_lp_postal_code, anu_lp_social_security_number, arc_active_energy_total_import_t1_price__eur, arc_active_energy_total_import_t2_price__eur, arc_business_usage_fee_price__eur, arc_created_by_id, arc_created_on, arc_deleted_by_id, arc_deleted_on, arc_is_deleted, arc_last_updated_by_id, arc_last_updated_on, arc_renewable_energy_fee_price__eur, arc_tax_rate__percent, supply_active_energy_total_import_t1fee_eur, supply_active_energy_total_import_t2fee_eur, supply_business_usage_fee_eur, supply_fee_total_eur, supply_renewable_energy_fee_eur, usage_active_energy_total_import_t0fee_eur, usage_active_energy_total_import_t1fee_eur, usage_active_energy_total_import_t2fee_eur, usage_active_power_total_import_t1peak_fee_eur, usage_fee_total_eur, usage_meter_fee_eur, usage_reactive_energy_total_ramped_t0fee_eur, bill_id, al_alti_biz_sub_project_code, anu_alti_biz_sub_project_code, tax_rate_percent, al_title, anu_title, arc_title) FROM stdin;
21	1	Invoice for DM at KINGCROSS	2024-10-01 14:18:25.01934+00	\N	2024-09-30 22:00:00+00	2024-10-31 23:00:00+00	\N	2024-04-20 11:06:03.068193+00	\N	\N	f	\N	2024-05-02 21:02:44.255836+00	1	2	\N	2024-04-20 11:16:00.724216+00	\N	\N	1	f	\N	2024-04-29 12:37:55.131879+00	3	4	1.42	10.96	12.38	test	test	test@dev.com	test	111111111	11111	11111111111	test	test	test@dev.com	test	111111111	11111	11111111111	0.1	0.06	0.0005	\N	2024-04-19 13:37:25.757982+00	\N	\N	f	\N	2024-04-29 13:11:14.047365+00	0.013936	13	0	0	0	0	0	0	0	0	0	10.96	10.96	0	\N	8045-1/21	8045-1/21	13			
22	1	Invoice for DM at KINGCROSS	2024-10-01 14:19:13.529776+00	\N	2024-09-30 22:00:00+00	2024-10-31 23:00:00+00	\N	2024-04-20 11:06:03.068193+00	\N	\N	f	\N	2024-05-02 21:02:44.255836+00	1	2	\N	2024-04-20 11:16:00.724216+00	\N	\N	1	f	\N	2024-04-29 12:37:55.131879+00	3	4	1.42	10.96	12.38	test	test	test@dev.com	test	111111111	11111	11111111111	test	test	test@dev.com	test	111111111	11111	11111111111	0.1	0.06	0.0005	\N	2024-04-19 13:37:25.757982+00	\N	\N	f	\N	2024-04-29 13:11:14.047365+00	0.013936	13	0	0	0	0	0	0	0	0	0	10.96	10.96	0	\N	8045-1/21	8045-1/21	13			
\.


--
-- Data for Name: network_user_calculations; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.network_user_calculations (id, issued_on, issued_by_id, from_date, to_date, meter_id, network_user_measurement_location_id, total_eur, title, usage_network_user_catalogue_id, network_user_invoice_id, kind, aunuc_created_by_id, aunuc_created_on, aunuc_deleted_by_id, aunuc_deleted_on, aunuc_is_deleted, aunuc_last_updated_by_id, aunuc_last_updated_on, anuml_created_by_id, anuml_created_on, anuml_deleted_by_id, anuml_deleted_on, anuml_is_deleted, anuml_last_updated_by_id, anuml_last_updated_on, anuml_meter_id, am_connection_power__w, am_created_by_id, am_created_on, am_deleted_by_id, am_deleted_on, am_is_deleted, am_last_updated_by_id, am_last_updated_on, am_messenger_id, mnt_min_kwh, mnt_max_kwh, mnt_amount_kwh, svt_price_eur, svt_total_eur, mvt_min_kwh, mvt_max_kwh, mvt_amount_kwh, mnt_price_eur, mnt_total_eur, svt_peak_kw, svt_amount_kw, usage_meter_fee_price_eur, usage_meter_total_eur, mjt_amount_kwh, mjt_max_kwh, mjt_min_kwh, mvt_price_eur, mvt_total_eur, asrc_active_energy_total_import_t1_price__eur, asrc_active_energy_total_import_t2_price__eur, asrc_business_usage_fee_price__eur, asrc_created_by_id, asrc_created_on, asrc_deleted_by_id, asrc_deleted_on, asrc_is_deleted, asrc_last_updated_by_id, asrc_last_updated_on, asrc_renewable_energy_fee_price__eur, asrc_tax_rate__percent, jen_reactive_export_amount_kvarh, jen_reactive_export_max_kvarh, jen_reactive_export_min_kvarh, jen_reactive_import_amount_kvarh, jen_reactive_import_max_kvarh, jen_reactive_import_min_kvarh, jen_ramped_amount_kvarh, jen_ramped_price_eur, jen_ramped_total_eur, supply_regulatory_catalogue_id, rvt_amount_kwh, rvt_max_kwh, rvt_min_kwh, rvt_price_eur, rvt_total_eur, rnt_amount_kwh, rnt_max_kwh, rnt_min_kwh, rnt_price_eur, rnt_total_eur, oie_max_kwh, trp_price_eur, trp_total_eur, supply_fee_total_eur, oie_min_kwh, oie_price_eur, oie_total_eur, mjt_price_eur, mjt_total_eur, usage_fee_total_eur, usage_meter_fee_amount, aunuc_active_energy_total_import_t0_price__eur, aunuc_active_energy_total_import_t1_price__eur, aunuc_active_energy_total_import_t2_price__eur, aunuc_active_power_total_import_t1_price__eur, aunuc_meter_fee_price__eur, aunuc_reactive_energy_total_ramped_t0_price__eur, trp_amount_kwh, trp_max_kwh, trp_min_kwh, oie_amount_kwh, jen_active_import_amount_kwh, jen_active_import_max_kwh, jen_active_import_min_kwh, am_phases, am_title, anuml_title, asrc_title, aunuc_title) FROM stdin;
41	2024-10-01 14:18:25.019266+00	\N	2024-09-30 22:00:00+00	2024-10-31 23:00:00+00	abb-B2x-1624226	1	5.48	test calculation for DM at KINGCROSS	1	21	RedLowNetworkUserCalculationEntity	\N	2024-04-19 13:37:35.162942+00	\N	\N	f	\N	2024-04-29 12:44:31.85688+00	\N	2024-04-22 16:45:20.621524+00	\N	\N	f	\N	\N	abb-B2x-1624226	24000	\N	2024-04-20 11:21:08.1314+00	\N	\N	f	\N	2024-04-22 16:43:24.823248+00	pidgeon	0	0	0	5.176	0	0	0	0	0.013272	0	0	0	5.481	5.48	0.0	0.0	0.0	0.029199	0	0.1	0.06	0.0005	\N	2024-04-19 13:37:25.757982+00	\N	\N	f	\N	2024-04-29 13:11:14.047365+00	0.013936	13	0	0	0	0	0	0	0	0.021236	0	1	0	0	0	0.1	0	0	0	0	0.06	0	0	0.0005	0	0	0	0.013936	0	0.0	0.0	5.48	1	0.0	0.029199	0.013272	5.176	5.481	0.021236	0	0	0	0	0	0	0	{l1,l2,l3}				\N
43	2024-10-01 14:19:13.529703+00	\N	2024-09-30 22:00:00+00	2024-10-31 23:00:00+00	abb-B2x-1624226	1	5.48	test calculation for DM at KINGCROSS	1	22	RedLowNetworkUserCalculationEntity	\N	2024-04-19 13:37:35.162942+00	\N	\N	f	\N	2024-04-29 12:44:31.85688+00	\N	2024-04-22 16:45:20.621524+00	\N	\N	f	\N	\N	abb-B2x-1624226	24000	\N	2024-04-20 11:21:08.1314+00	\N	\N	f	\N	2024-04-22 16:43:24.823248+00	pidgeon	0	0	0	5.176	0	0	0	0	0.013272	0	0	0	5.481	5.48	0.0	0.0	0.0	0.029199	0	0.1	0.06	0.0005	\N	2024-04-19 13:37:25.757982+00	\N	\N	f	\N	2024-04-29 13:11:14.047365+00	0.013936	13	0	0	0	0	0	0	0	0.021236	0	1	0	0	0	0.1	0	0	0	0	0.06	0	0	0.0005	0	0	0	0.013936	0	0.0	0.0	5.48	1	0.0	0.029199	0.013272	5.176	5.481	0.021236	0	0	0	0	0	0	0	{l1,l2,l3}				\N
42	2024-10-01 14:18:25.019297+00	\N	2024-09-30 22:00:00+00	2024-10-31 23:00:00+00	schneider-iEM3xxx-19453067	2	5.48	test calculation for DM at KINGCROSS	2	21	BlueLowNetworkUserCalculationEntity	\N	2024-04-19 13:37:43.769977+00	\N	\N	f	\N	2024-04-29 12:45:42.845717+00	\N	2024-04-22 16:45:48.280561+00	\N	\N	f	\N	\N	schneider-iEM3xxx-19453067	24000	\N	2024-04-20 11:21:41.28222+00	\N	\N	f	\N	2024-04-22 16:43:36.740555+00	pidgeon	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0	5.481	5.48	0	0	0	0.0	0.0	0.1	0.06	0.0005	\N	2024-04-19 13:37:25.757982+00	\N	\N	f	\N	2024-04-29 13:11:14.047365+00	0.013936	13	0	0	0	0	0	0	0	0.021236	0	1	0	0	0	0.1	0	0	0	0	0.06	0	0	0.0005	0	0	0	0.013936	0	0.041144	0	5.48	1	0.041144	0.0	0.0	0.0	5.481	0.021236	0	0	0	0	0	0	0	{l1,l2,l3}				\N
44	2024-10-01 14:19:13.529721+00	\N	2024-09-30 22:00:00+00	2024-10-31 23:00:00+00	schneider-iEM3xxx-19453067	2	5.48	test calculation for DM at KINGCROSS	2	22	BlueLowNetworkUserCalculationEntity	\N	2024-04-19 13:37:43.769977+00	\N	\N	f	\N	2024-04-29 12:45:42.845717+00	\N	2024-04-22 16:45:48.280561+00	\N	\N	f	\N	\N	schneider-iEM3xxx-19453067	24000	\N	2024-04-20 11:21:41.28222+00	\N	\N	f	\N	2024-04-22 16:43:36.740555+00	pidgeon	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0	0.0	5.481	5.48	0	0	0	0.0	0.0	0.1	0.06	0.0005	\N	2024-04-19 13:37:25.757982+00	\N	\N	f	\N	2024-04-29 13:11:14.047365+00	0.013936	13	0	0	0	0	0	0	0	0.021236	0	1	0	0	0	0.1	0	0	0	0	0.06	0	0	0.0005	0	0	0	0.013936	0	0.041144	0	5.48	1	0.041144	0.0	0.0	0.0	5.481	0.021236	0	0	0	0	0	0	0	{l1,l2,l3}				\N
\.


--
-- Data for Name: network_user_invoice_states; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.network_user_invoice_states (correlation_id, current_state, network_user_invoice_id, bill_id, cancel_reason) FROM stdin;
8eee0000-2362-309c-a5d7-08dce223ea33	Cancelled	21	\N	Fake cancelled
8eee0000-2362-309c-9077-08dce22407b6	Registered	22	f13ea69f-b14a-4e99-ba3c-d3c9d222fa71	\N
\.


--
-- Data for Name: network_user_representatives; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.network_user_representatives (network_user_id, representative_id) FROM stdin;
1	41qe2wwv2xxf7wkzj43mfb9sns
\.


--
-- Data for Name: notifications; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.notifications (id, "timestamp", event_id, title, summary, content, kind, resolved_by_id, resolved_on, messenger_id, topics, invoice_id) FROM stdin;
\.


--
-- Data for Name: notification_recipients; Type: TABLE DATA; Schema: public; Owner: ozds
--

COPY public.notification_recipients (representative_id, notification_id, seen_on) FROM stdin;
\.


--
-- Name: catalogues_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.catalogues_id_seq', 1, false);


--
-- Name: events_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.events_id_seq', 40, true);


--
-- Name: inbox_state_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.inbox_state_id_seq', 1, false);


--
-- Name: location_invoices_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.location_invoices_id_seq', 1, false);


--
-- Name: locations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.locations_id_seq', 1, true);


--
-- Name: measurement_locations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.measurement_locations_id_seq', 2, true);


--
-- Name: measurement_validators_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.measurement_validators_id_seq', 2, true);


--
-- Name: network_user_calculations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.network_user_calculations_id_seq', 44, true);


--
-- Name: network_user_catalogues_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.network_user_catalogues_id_seq', 5, true);


--
-- Name: network_user_invoices_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.network_user_invoices_id_seq', 22, true);


--
-- Name: network_users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.network_users_id_seq', 1, true);


--
-- Name: notifications_id_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.notifications_id_seq', 5, true);


--
-- Name: outbox_message_sequence_number_seq; Type: SEQUENCE SET; Schema: public; Owner: ozds
--

SELECT pg_catalog.setval('public.outbox_message_sequence_number_seq', 1, false);


--
-- PostgreSQL database dump complete
--


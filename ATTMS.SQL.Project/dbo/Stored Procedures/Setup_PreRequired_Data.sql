
-- =============================================
-- Author:    	<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.Setup_PreRequired_Data
AS
BEGIN
  SET NOCOUNT ON;

  INSERT [dbo].[AspNetRoles] ([Id], [Name])
    VALUES (N'1', N'Administrator')

  INSERT [dbo].[AspNetRoles] ([Id], [Name])
    VALUES (N'2', N'Operator')

  INSERT [dbo].[AspNetRoles] ([Id], [Name])
    VALUES (N'3', N'Runner')

  INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])
    VALUES (N'82683e8e-415a-42d9-8ebe-e10b87dcce93', N'ismail@noksmart.com', 0, N'AEGhmCVZTFFcAhoMAvss/z0YrfwU1NOmgsr0jlVgprqrobAAeLQNzz+BuTYeJR6p4w==', N'7c4a2cd8-e20a-419d-9c3c-0931c7e24bad', NULL, 0, 0, NULL, 0, 0, N'Admin')


  SET IDENTITY_INSERT [dbo].[documents_ref] ON


  INSERT [dbo].[documents_ref] ([Id], [document_type_title], [icon], [valid], [expiration_days], [required])
    VALUES (1, N'Contract', N'fa fa-file-pdf-o text-info', 1, 365, 1)

  SET IDENTITY_INSERT [dbo].[documents_ref] OFF

  SET IDENTITY_INSERT [dbo].[job_title] ON


  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (1, N'Develper')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (2, N'Account Executive')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (3, N'Administrative Assistant')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (4, N'Administrative Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (5, N'Branch Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (6, N'Business Analyst')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (7, N'Business Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (8, N'Chief Executive Officer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (9, N'Office Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (10, N'Operations Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (11, N'Quality Control Coordinator')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (12, N'Risk Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (13, N'Service Representative')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (14, N'Accounts Receivable/Payable Specialist')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (15, N'Assessor')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (16, N'Auditor')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (17, N'Bookkeeper')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (18, N'Budget Analyst')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (19, N'Cash Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (20, N'Chief Financial Officer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (21, N'Controller')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (22, N'Credit Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (23, N'Tax Specialist')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (24, N'Treasurer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (25, N'Certified Financial Planner')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (26, N'Chartered Wealth Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (27, N'Credit Analyst')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (28, N'Credit Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (29, N'Financial Analyst')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (30, N'Hedge Fund Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (31, N'Hedge Fund Principal')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (32, N'Hedge Fund Trader')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (33, N'Investment Advisor')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (34, N'Investment Banker')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (35, N'Investor Relations Officer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (36, N'Leveraged Buyout Investor')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (37, N'Loan Officer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (38, N'Mortgage Banker')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (39, N'Mutual Fund Analyst')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (40, N'Portfolio Management Marketing')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (41, N'Portfolio Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (42, N'Ratings Analyst')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (43, N'Stockbroker')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (44, N'Trust Officer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (45, N'Benefits Officer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (46, N'Compensation Analyst')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (47, N'Employee Relations Specialist')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (48, N'HR Coordinator')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (49, N'HR Specialist')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (50, N'Retirement Plan Counselor')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (51, N'Staffing Consultant')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (52, N'Union Organizer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (53, N'Business Systems Analyst')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (54, N'Content Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (55, N'Content Strategist')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (56, N'Database Administrator')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (57, N'Digital Marketing Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (58, N'Full Stack Developer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (59, N'Information Architect')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (60, N'Marketing Technologist')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (61, N'Mobile Developer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (62, N'Project Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (63, N'Social Media Manager')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (64, N'Software Engineer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (65, N'Systems Engineer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (66, N'Software Developer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (67, N'Systems Administrator')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (68, N'User Interface Specialist')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (69, N'Web Analytics Developer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (70, N'Web Developer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (71, N'Webmaster')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (72, N'Actuary')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (73, N'Claims Adjuster')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (74, N'Damage Appraiser')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (75, N'Insurance Adjuster')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (76, N'Insurance Agent')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (77, N'Insurance Appraiser')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (78, N'Insurance Broker')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (79, N'Insurance Claims Examiner')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (80, N'Insurance Investigator')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (81, N'Loss Control Specialist')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (82, N'Underwriter')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (83, N'Business Broker')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (84, N'Business Transfer Agent')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (85, N'Commercial Appraiser')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (86, N'Commercial Real Estate Agent')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (87, N'Commercial Real Estate Broker')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (88, N'Real Estate Appraiser')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (89, N'Real Estate Officer')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (90, N'Residential Appraiser')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (91, N'Residential Real Estate Agent')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (92, N'Residential Real Estate Broker')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (93, N'sswqsqws')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (97, N'aa1')

  INSERT [dbo].[job_title] ([Id], [title])
    VALUES (98, N'qqqq')

  SET IDENTITY_INSERT [dbo].[job_title] OFF

  SET IDENTITY_INSERT [dbo].[leave_request_ref] ON


  INSERT [dbo].[leave_request_ref] ([Id], [leave_request_title], [open_balance_defualt], [is_default], [valid], [starting_from_days])
    VALUES (2, N'illness', 2, 0, 1, 2)

  SET IDENTITY_INSERT [dbo].[leave_request_ref] OFF

  SET IDENTITY_INSERT [dbo].[setting] ON


  INSERT [dbo].[setting] ([Id], [group_name], [key_name], [value])
    VALUES (2, N'Attendance', N'deduction rate', N'1.5')

  SET IDENTITY_INSERT [dbo].[setting] OFF

  SET IDENTITY_INSERT [dbo].[shift_rule_type] ON


  INSERT [dbo].[shift_rule_type] ([Id], [title])
    VALUES (1, N'Deduction')

  INSERT [dbo].[shift_rule_type] ([Id], [title])
    VALUES (2, N'Over Time')

  INSERT [dbo].[shift_rule_type] ([Id], [title])
    VALUES (3, N'Miss CheckIn')

  INSERT [dbo].[shift_rule_type] ([Id], [title])
    VALUES (4, N'Miss CheckOut')

  INSERT [dbo].[shift_rule_type] ([Id], [title])
    VALUES (5, N'CheckIn')

  INSERT [dbo].[shift_rule_type] ([Id], [title])
    VALUES (6, N'CheckOut')

  INSERT [dbo].[shift_rule_type] ([Id], [title])
    VALUES (7, N'Abscence')

  INSERT [dbo].[shift_rule_type] ([Id], [title])
    VALUES (8, N'fsdfs')

  SET IDENTITY_INSERT [dbo].[shift_rule_type] OFF

END
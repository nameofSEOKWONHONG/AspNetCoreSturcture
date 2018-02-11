1. database 생성 및 table 생성
2. nuget package download
	1) Install-Package Microsoft.EntityFrameworkCore.SqlServer
	2) Install-Package Microsoft.EntityFrameworkCore.Tools
	3) Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design
3. nuget package console
	1) Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=[database name];Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
4. models 폴더 이하에 관련 파일 생성
5. dbcontext는 constructor 변경 및 외부 파일로 변경함.
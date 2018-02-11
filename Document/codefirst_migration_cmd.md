1. 관련 페키지 추가 csproj
<ItemGroup>
  <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
  <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.0" />
</ItemGroup>

2. 마이그레이션 
	1) Add-Migration InitialCreate 실행.
	2) dbcontext가 외부 프로젝트에 있을 경우 Add-Migration -Context [Context명] InitialCreate 실행
	3) migrations 폴더에 관련 사항 생성 됨.
	4) update-database 실행함.
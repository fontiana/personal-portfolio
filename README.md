# PersonalPortfolio

| Travis CI     | Codecov       | Code Climate   |
|:-------------:|:-------------:|:--------------:|
|  [![devDependencies](https://travis-ci.org/fontiana/Personal-Portfolio.svg?branch=master)](https://api.travis-ci.org/fontiana/PersonalPortfolio.svg?branch=master) | [![Coverage Status](https://coveralls.io/repos/github/fontiana/personal-portfolio/badge.svg?branch=master&cache=3)](https://coveralls.io/github/fontiana/personal-portfolio?branch=master) | [![Maintainability](https://api.codeclimate.com/v1/badges/319770019ac2bd77d042/maintainability)](https://codeclimate.com/github/fontiana/personal-portfolio/maintainability) |

My personal portfolio and blog

## How it works

- Built with DOT.NET Core 3.1
- It's deployed on Azure Web App using Kudu tools which watchs for changes on this repository
- It could also be deployed using this command:

   ```shell
   cd PersonalPortfolio
   dotnet publish -c Release -o /Users/{user}/Documents/Projetos/Deploys
   ```

## Work in Progress

- [Entity Framework Core](https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-3.1&tabs=visual-studio)
- [Repository Pattern](https://codewithshadman.com/repository-pattern-csharp/)
- [Unit Testing](https://docs.microsoft.com/pt-br/aspnet/core/mvc/controllers/testing?view=aspnetcore-3.1)
- [Admin Menu](https://bootstrapious.com/p/bootstrap-sidebar)
- [Localisation :white_check_mark:](https://andrewlock.net/adding-localisation-to-an-asp-net-core-application/)
- [Adding security headers](https://andrewlock.net/adding-default-security-headers-in-asp-net-core/)
- [Checking Security headers](https://securityheaders.com/?q=www.victorfontana.com.br&followRedirects=on)
- [Settings SSL :white_check_mark:](https://rajbos.github.io/blog/2019/08/27/LetsEncrypt-Windows)

### Design Tips

- [Design better forms](https://uxdesign.cc/design-better-forms-96fadca0f49c)

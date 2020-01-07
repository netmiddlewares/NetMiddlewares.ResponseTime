# NetMiddlewares.ResponseTime

[![GitHub license](https://img.shields.io/github/license/netmiddlewares/NetMiddlewares.ResponseTime)](https://github.com/netmiddlewares/NetMiddlewares.ResponseTime)
[![Build Status](https://travis-ci.org/netmiddlewares/NetMiddlewares.ResponseTime.svg?branch=master)](https://travis-ci.org/netmiddlewares/NetMiddlewares.ResponseTime)
[![Nuget](https://buildstats.info/nuget/NetMiddlewares.ResponseTime)](http://www.nuget.org/packages/NetMiddlewares.ResponseTime)

.NET Core middleware to get the response time and add it to a response header

# How to use

## Step 1 - Add the middleware to the services declaration

```
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddResponseTime();
}
  ```
  
## Step 2 - Add the middleware to the pipeline

```
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    //...
    
    app.UseResponseTime();
    
    //...
}
  ```

Done!

From now on, everytime the pipeline is run, the response will have the response time under the "X-Response-Time" header.

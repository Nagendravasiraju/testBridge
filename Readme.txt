Steps to Create: 

1. Run sqlScripts 
2. Change the following values in web.config 

    <add key="connstring" value="data source=<<serverIP>>;uid=<<Userid>>;pwd=<<Password>>;database=<<DbName>>;packet size=4096"/>
    <add key="BaseRootURL" value="//localhost:59250"/>
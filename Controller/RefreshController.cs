using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;


[Route("api/")]
[ApiController]
public class RefreshController: Controller{
    
    private dbcontextproduct db;
    private ILogger<RefreshController> log;
    public RefreshController(dbcontextproduct db,ILogger<RefreshController> log){this.db = db; this.log = log;}

    
    
    [HttpPost("Oauth")]
    public IActionResult refr([FromBody]JWTModel mod){
        var token = db.Refresh.FirstOrDefault(p => p.Refreshtoken == mod.Refreshtoken);
        
        if (token == null){
            
            log.LogInformation(HttpContext.Connection.RemotePort.ToString());
               return Unauthorized("aaaaaaa");
               
        }
       if (!(token.Time.Day >= (DateTime.UtcNow - new TimeSpan(60,0,0,0)).Day)){
            db.Refresh.Remove(token);
            var newrefresh = new JWTdb() {Time = DateTime.UtcNow, Refreshtoken = JWTModel.rnd()};
            db.Refresh.Add(newrefresh);
            db.SaveChanges();
            var handler = new JwtSecurityTokenHandler();
            var token4 = handler.ReadJwtToken(mod.Accesstoken);
            var claims = token4.Claims;
            
            return Json( new JWTModel() {Accesstoken = JWTModel.jwtcreator(claims.First(p => p.Type == "Name").Value,claims.First(p => p.Type == "Email").Value)
            , Refreshtoken = newrefresh.Refreshtoken  } );
       }
       if (token.Time.Day >= (DateTime.UtcNow - new TimeSpan(60,0,0,0)).Day)
               db.Refresh.Remove(token);
               db.SaveChanges();
        log.LogInformation(HttpContext.Request.Host.Value);
       return Unauthorized("не автор22222");
    }
}
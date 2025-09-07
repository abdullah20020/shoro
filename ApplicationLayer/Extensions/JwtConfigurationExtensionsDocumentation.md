# JWT Configuration Extensions Documentation

## نظرة عامة
تم فصل جميع إعدادات JWT في ملف منفصل لتنظيم الكود وتحسين القابلية للصيانة.

## الملفات المُنشأة

### 1. JwtConfigurationExtensions.cs
يحتوي على جميع extension methods لإعداد JWT:

```csharp
// الإعداد الرئيسي
builder.Services.AddJwtConfiguration(configuration);

// إعداد Identity
builder.Services.AddIdentityConfiguration<BaseUser, IdentityRole>();

// إعداد Authorization Policies
builder.Services.AddAuthorizationPolicies();
```

## المميزات

### ✅ **AddJwtConfiguration**
- إعداد JWT Settings من التكوين
- إعداد JWT Authentication
- تسجيل JWT Services

### ✅ **AddIdentityConfiguration**
- إعداد كلمات المرور
- إعداد المستخدمين
- إعداد Lockout
- إعداد SignIn

### ✅ **AddAuthorizationPolicies**
- AdminOnly
- LawyerOnly
- LawFirmOnly
- ClientOnly
- LawyerOrLawFirm
- AdminOrLawyer
- AuthenticatedUser

## قبل التحديث (Program.cs)

```csharp
// Configure Identity
builder.Services.AddIdentity<BaseUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ShoraDbContext>()
.AddDefaultTokenProviders();

// Configure JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var key = Encoding.ASCII.GetBytes(jwtSettings?.SecretKey ?? "default-key");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings?.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings?.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Configure Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("LawyerOnly", policy => policy.RequireRole("Lawyer"));
    options.AddPolicy("LawFirmOnly", policy => policy.RequireRole("LawFirm"));
    options.AddPolicy("ClientOnly", policy => policy.RequireRole("Client"));
});

// JWT Service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
```

## بعد التحديث (Program.cs)

```csharp
// Configure Identity
builder.Services.AddIdentityConfiguration<BaseUser, IdentityRole>()
    .AddEntityFrameworkStores<ShoraDbContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication and Services
builder.Services.AddJwtConfiguration(builder.Configuration);

// Configure Authorization Policies
builder.Services.AddAuthorizationPolicies();
```

## المميزات الجديدة

### 🎯 **تنظيم أفضل**
- فصل إعدادات JWT في ملف منفصل
- كود أكثر تنظيماً ووضوحاً
- سهولة الصيانة والتطوير

### 🔧 **مرونة في التكوين**
- إمكانية تخصيص إعدادات Identity
- إعدادات JWT قابلة للتكوين
- سياسات Authorization مرنة

### 📦 **إعادة الاستخدام**
- يمكن استخدام الـ extensions في مشاريع أخرى
- سهولة الاختبار
- فصل الاهتمامات (Separation of Concerns)

## الاستخدام في Controllers

```csharp
[Authorize(Policy = "AdminOnly")]
public IActionResult AdminOnlyEndpoint()
{
    return Ok("Admin only access");
}

[Authorize(Policy = "LawyerOrLawFirm")]
public IActionResult LawyerAccessEndpoint()
{
    return Ok("Lawyer or Law Firm access");
}
```

## التكوين المطلوب

### appsettings.json
```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong",
    "Issuer": "ShoraAPI",
    "Audience": "ShoraUsers",
    "ExpirationMinutes": 60
  }
}
```

## الفوائد

1. **كود أنظف**: Program.cs أصبح أكثر وضوحاً
2. **سهولة الصيانة**: جميع إعدادات JWT في مكان واحد
3. **إعادة الاستخدام**: يمكن استخدام الـ extensions في مشاريع أخرى
4. **اختبار أسهل**: يمكن اختبار كل extension منفصلاً
5. **مرونة أكبر**: إمكانية تخصيص الإعدادات بسهولة

## ملاحظات مهمة

- تم إزالة الـ using statements غير المستخدمة
- تم إزالة تسجيل JWT service المكرر
- جميع الوظائف تعمل كما هي بدون تغيير
- الكود أصبح أكثر تنظيماً وقابلية للقراءة

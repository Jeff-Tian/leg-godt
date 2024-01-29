using System.Net;
using System.Text;
using ApiTest.Fixtures;
using FluentAssertions;

namespace ApiTest;

[TestClass]
public class WebhookTest : TestBase
{
    [TestMethod]
    public async Task TestWebhook()
    {
        var response = await _httpClient!.PostAsync("/api/webhook/yuque",
            new StringContent("{\"data\":{\"title\":\"test\",\"body_html\":\"test_body_html\",\"format\":\"lake\"}}", Encoding.UTF8,
                "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        response.Content.Headers.ContentType?.MediaType.Should().Be("text/plain");
        var body = await response.Content.ReadAsStringAsync();
        body.Should().Be("test_body_html");
    }

    [TestMethod]
    public async Task TestYuquePublish()
    {
        var response = await _httpClient!.PostAsync("/api/webhook/yuque",
            new StringContent(this._publishedYuqueArticle, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    private string _publishedYuqueArticle = @"{""data"":{
  ""id"": 157176225,
  ""slug"": ""azl1phhstd3euu1w"",
  ""title"": ""基于 Keycloak 的关注微信公众号即登录方案再次升级：有意思的成长"",
  ""book_id"": 458374,
  ""user_id"": 221736,
  ""format"": ""lake"",
  ""body"": ""几年前出于个人兴趣，基于 Keycloak 做了一个关注微信公众号即登录的方案，见《[基于 keycloak 的关注公众号即登录功能的设计与实现 - Jeff Tian的文章 - 知乎]([https://zhuanlan.zhihu.com/p/349504145](https://zhuanlan.zhihu.com/p/349504145)) 》，它是基于一个大佬写的 [https://github.com/jyqq163/keycloak-services-social-weixin](https://github.com/jyqq163/keycloak-services-social-weixin) Keycloak 插件，进行了一番魔改而成的。日子一天一天过去，没想到，我的这个魔改版本，star 数量居然开始超过原版了。<br />![star-history-2024129.png](https://cdn.nlark.com/yuque/0/2024/png/221736/1706502201963-d6888950-aff6-47c5-b77e-2b705c4913d9.png#averageHue=%23fafaf9&clientId=u852aa60d-6f80-4&from=ui&id=uca5c2b15&originHeight=2890&originWidth=4016&originalType=binary&ratio=2&rotation=0&showTitle=false&size=467279&status=done&style=none&taskId=u61be5048-f92a-40e3-9a3a-0aacfd88f7c&title=)\n\n由于自己主要使用语言是 nodejs，对于 Java 并不太熟悉，所以原版对我帮助很大，不然，我根本不知道如何下手开始为 Keycloak 写插件。虽然写得很烂，但的确有一定的商业价值，因为通过关注公众号登录，比起普通的扫码登录，无论是对用户，还是对开发，都更友好。<br />我想就是因为这个小小的商业价值，导致这个小插件，被越来越多的人关注，并被多家公司正式商用（比如“答疑家”）。由于有用户，所以我也陆续不断对它进行迭代升级，并在这个过程中，不断学习 Java 以及微信生态，这是一个**有趣的成长过程**。<br />更有趣的是，我最近尝试在自己的应用中加入 GenAI，其中之一就是在微信公众号中，利用 GenAI 进行自动回复（《[欢迎来调戏我：在公众号里对接 AWS Bedrock 服务 - Jeff Tian的文章 - 知乎]([https://zhuanlan.zhihu.com/p/674125115](https://zhuanlan.zhihu.com/p/674125115)) 》），因此稍稍学习了一下微信的消息接口，最终发现又可以将这个学习成果应用在本 Keycloak 微信插件当中，进一步提升开发者体验，这就是**更加有趣的一点了**。\n<a name=\""SrLDj\""></a>\n# 体验\n可以通过这个链接 [https://keycloak.jiwai.win/realms/Brickverse/account/#/](https://keycloak.jiwai.win/realms/Brickverse/account/#/) 进行体验，在 PC Web 端选择微信登录方式即可。注意上述链接中使用了我个人的测试公众号，只能支持 100 个用户。\n<a name=\""gMf2v\""></a>\n# 现状\n在《[【继续更新】尝试在 Keycloak 里打通整个微信生态 - Jeff Tian的文章 - 知乎]([https://zhuanlan.zhihu.com/p/652566471](https://zhuanlan.zhihu.com/p/652566471)) 》里，介绍了该 Keycloak 微信登录插件，支持三种登录方式，分别是 ① 关注公众号即登录；② 基于开放平台的 OAuth 2.0 扫码登录；③ 手机端基于公众号的 OAuth 2.0 方式登录。但是，对于第 ① 种方式，配置方式其实是很麻烦的，这是因为需要开发者自己去将微信服务器发送过来的带参二维码扫描或者关注的 xml 消息，转发给 keycloak 服务器。也就是说需要开发者自己实现和微信服务器的对接。<br />具体来说，需要开发者在接收到微信服务器发来的 xml 消息后，将消息原封不动地转发给 Keycloak 服务器的 `/realms/<realm>/QrCodeResourceProviderFactory/mp-qr-scan-status`接口，比如这样完整 URL：`[https://keycloak.jiwai.win/realms/Brickverse/QrCodeResourceProviderFactory/mp-qr-scan-status](https://keycloak.jiwai.win/realms/Brickverse/QrCodeResourceProviderFactory/mp-qr-scan-status)`。要注意的是，需要在微信公众号后台配置自己的消息处理服务接口，直接配置 Keycloak 的是不行的。因为以上 Keycloak 接口只用来接收微信扫码事件，并没有实现微信消息接口协议，直接配置上去微信是不认的，会报错：<br />![image.png](https://cdn.nlark.com/yuque/0/2024/png/221736/1706504357304-8f7f6c17-f2e2-40d5-9619-989948693027.png#averageHue=%23fbfaf7&clientId=u852aa60d-6f80-4&from=paste&height=792&id=u613d2773&originHeight=1584&originWidth=1852&originalType=binary&ratio=2&rotation=0&showTitle=false&size=594229&status=done&style=none&taskId=u0d4a128b-bfd9-4bad-9597-0e44dd89f82&title=&width=926)<br />所以这就要求开发者自己实现和微信的消息对接，这可能需要一定的开发，所以是高级用法。当然，自己和微信消息对接**也是有好处的，比如可以针对消息做更多的业务逻辑处理**，只是同时需要将它转发给 Keycloak 而已。<br />但如果并没有高级需求，那么如果可以直接将 Keycloak 的 URL 配置上去，显然会更加方便，省去了自己的开发步骤。\n<a name=\""CigeI\""></a>\n# 改进\n因为自己对微信的消息机制又了解得更多了，所以直接在 Keycloak 里对接了微信的消息接口，这样就可以进一步简化开发者的接入了。但同时，老的转发方式也还在，也就是变成了高级接入方式。通过高级接入，开发者可以基于微信的消息做更多的事情。<br />通过使用新版本 [0.5.13]([https://github.com/Jeff-Tian/keycloak-services-social-weixin/releases/tag/0.5.13](https://github.com/Jeff-Tian/keycloak-services-social-weixin/releases/tag/0.5.13) ) 中，就可以省去自己开发微信消息接口的步骤，直接配置 Keycloak 插件里自带的，即：`/realms/<realm>/QrCodeResourceProviderFactory/message`接口。比如我在体验链接实例里，就是这样配置的：<br />![image.png](https://cdn.nlark.com/yuque/0/2024/png/221736/1706504624416-0baa49a0-9b35-4417-b3c1-2b27a284227b.png#averageHue=%23f2f2f2&clientId=u852aa60d-6f80-4&from=paste&height=159&id=u0e228d0e&originHeight=318&originWidth=2226&originalType=binary&ratio=2&rotation=0&showTitle=false&size=64432&status=done&style=none&taskId=u9bc08bbb-125f-4fc5-8078-8c959f9db34&title=&width=1113)\n<a name=\""hTp9D\""></a>\n# 待办事项 \n不过，这个关注公众号即登录，还有更多值得优化的地方，比如需要支持分布式缓存等。现在这个插件，只能用在单个 Keycloak 实例中，如果要横向扩展，就需要引入 Redis 或者其他数据库来对扫码状态做存储。\n"",
  ""body_draft"": """",
  ""body_html"": ""<!doctype html><div class=\""lake-content\"" typography=\""classic\""><p id=\""u8478b11d\"" class=\""ne-p\""><span class=\""ne-text\"">几年前出于个人兴趣，基于 Keycloak 做了一个关注微信公众号即登录的方案，见《[基于 keycloak 的关注公众号即登录功能的设计与实现 - Jeff Tian的文章 - 知乎](</span><a href=\""https://zhuanlan.zhihu.com/p/349504145\"" data-href=\""https://zhuanlan.zhihu.com/p/349504145\"" target=\""_blank\"" class=\""ne-link\""><span class=\""ne-text\"">https://zhuanlan.zhihu.com/p/349504145</span></a><span class=\""ne-text\"">) 》，它是基于一个大佬写的 </span><a href=\""https://github.com/jyqq163/keycloak-services-social-weixin\"" data-href=\""https://github.com/jyqq163/keycloak-services-social-weixin\"" target=\""_blank\"" class=\""ne-link\""><span class=\""ne-text\"">https://github.com/jyqq163/keycloak-services-social-weixin</span></a><span class=\""ne-text\""> Keycloak 插件，进行了一番魔改而成的。日子一天一天过去，没想到，我的这个魔改版本，star 数量居然开始超过原版了。</span></p><p id=\""uda15a997\"" class=\""ne-p\""><img src=\""https://cdn.nlark.com/yuque/0/2024/png/221736/1706502201963-d6888950-aff6-47c5-b77e-2b705c4913d9.png\"" width=\""4016\"" id=\""uca5c2b15\"" class=\""ne-image\""></p><p id=\""ua16df3d4\"" class=\""ne-p\""><span class=\""ne-text\""></span></p><p id=\""uc89acea6\"" class=\""ne-p\""><span class=\""ne-text\"">由于自己主要使用语言是 nodejs，对于 Java 并不太熟悉，所以原版对我帮助很大，不然，我根本不知道如何下手开始为 Keycloak 写插件。虽然写得很烂，但的确有一定的商业价值，因为通过关注公众号登录，比起普通的扫码登录，无论是对用户，还是对开发，都更友好。</span></p><p id=\""u7ce4ac25\"" class=\""ne-p\""><span class=\""ne-text\"">我想就是因为这个小小的商业价值，导致这个小插件，被越来越多的人关注，并被多家公司正式商用（比如“答疑家”）。由于有用户，所以我也陆续不断对它进行迭代升级，并在这个过程中，不断学习 Java 以及微信生态，这是一个</span><strong><span class=\""ne-text\"">有趣的成长过程</span></strong><span class=\""ne-text\"">。</span></p><p id=\""uf5ce43f4\"" class=\""ne-p\""><span class=\""ne-text\"">更有趣的是，我最近尝试在自己的应用中加入 GenAI，其中之一就是在微信公众号中，利用 GenAI 进行自动回复（《[欢迎来调戏我：在公众号里对接 AWS Bedrock 服务 - Jeff Tian的文章 - 知乎](</span><a href=\""https://zhuanlan.zhihu.com/p/674125115\"" data-href=\""https://zhuanlan.zhihu.com/p/674125115\"" target=\""_blank\"" class=\""ne-link\""><span class=\""ne-text\"">https://zhuanlan.zhihu.com/p/674125115</span></a><span class=\""ne-text\"">) 》），因此稍稍学习了一下微信的消息接口，最终发现又可以将这个学习成果应用在本 Keycloak 微信插件当中，进一步提升开发者体验，这就是</span><strong><span class=\""ne-text\"">更加有趣的一点了</span></strong><span class=\""ne-text\"">。</span></p><h1 id=\""SrLDj\""><span class=\""ne-text\"">体验</span></h1><p id=\""uf217b511\"" class=\""ne-p\""><span class=\""ne-text\"">可以通过这个链接 </span><a href=\""https://keycloak.jiwai.win/realms/Brickverse/account/#/\"" data-href=\""https://keycloak.jiwai.win/realms/Brickverse/account/#/\"" target=\""_blank\"" class=\""ne-link\""><span class=\""ne-text\"">https://keycloak.jiwai.win/realms/Brickverse/account/#/</span></a><span class=\""ne-text\""> 进行体验，在 PC Web 端选择微信登录方式即可。</span><span class=\""ne-text\"" style=\""color: rgb(25, 27, 31)\"">注意上述链接中使用了我个人的测试公众号，只能支持 100 个用户。</span></p><h1 id=\""gMf2v\""><span class=\""ne-text\"">现状</span></h1><p id=\""ue42d2d6f\"" class=\""ne-p\""><span class=\""ne-text\"">在《[【继续更新】尝试在 Keycloak 里打通整个微信生态 - Jeff Tian的文章 - 知乎](</span><a href=\""https://zhuanlan.zhihu.com/p/652566471\"" data-href=\""https://zhuanlan.zhihu.com/p/652566471\"" target=\""_blank\"" class=\""ne-link\""><span class=\""ne-text\"">https://zhuanlan.zhihu.com/p/652566471</span></a><span class=\""ne-text\"">) 》里，介绍了该 Keycloak 微信登录插件，支持三种登录方式，分别是 ① 关注公众号即登录；② 基于开放平台的 OAuth 2.0 扫码登录；③ 手机端基于公众号的 OAuth 2.0 方式登录。但是，对于第 ① 种方式，配置方式其实是很麻烦的，这是因为需要开发者自己去将微信服务器发送过来的带参二维码扫描或者关注的 xml 消息，转发给 keycloak 服务器。也就是说需要开发者自己实现和微信服务器的对接。</span></p><p id=\""uf292c2f6\"" class=\""ne-p\""><span class=\""ne-text\"">具体来说，需要开发者在接收到微信服务器发来的 xml 消息后，将消息原封不动地转发给 Keycloak 服务器的 </span><code class=\""ne-code\""><span class=\""ne-text\"">/realms/&lt;realm&gt;/QrCodeResourceProviderFactory/mp-qr-scan-status</span></code><span class=\""ne-text\"">接口，比如这样完整 URL：`</span><a href=\""https://keycloak.jiwai.win/realms/Brickverse/QrCodeResourceProviderFactory/mp-qr-scan-status\"" data-href=\""https://keycloak.jiwai.win/realms/Brickverse/QrCodeResourceProviderFactory/mp-qr-scan-status\"" target=\""_blank\"" class=\""ne-link\""><span class=\""ne-text\"">https://keycloak.jiwai.win/realms/Brickverse/QrCodeResourceProviderFactory/mp-qr-scan-status</span></a><span class=\""ne-text\"">`。要注意的是，需要在微信公众号后台配置自己的消息处理服务接口，直接配置 Keycloak 的是不行的。因为以上 Keycloak 接口只用来接收微信扫码事件，并没有实现微信消息接口协议，直接配置上去微信是不认的，会报错：</span></p><p id=\""u9eea96bc\"" class=\""ne-p\""><img src=\""https://cdn.nlark.com/yuque/0/2024/png/221736/1706504357304-8f7f6c17-f2e2-40d5-9619-989948693027.png\"" width=\""926\"" id=\""u613d2773\"" class=\""ne-image\""></p><p id=\""ud9f8ae3f\"" class=\""ne-p\""><span class=\""ne-text\"">所以这就要求开发者自己实现和微信的消息对接，这可能需要一定的开发，所以是高级用法。当然，自己和微信消息对接</span><strong><span class=\""ne-text\"">也是有好处的，比如可以针对消息做更多的业务逻辑处理</span></strong><span class=\""ne-text\"">，只是同时需要将它转发给 Keycloak 而已。</span></p><p id=\""ue9bee15d\"" class=\""ne-p\""><span class=\""ne-text\"">但如果并没有高级需求，那么如果可以直接将 Keycloak 的 URL 配置上去，显然会更加方便，省去了自己的开发步骤。</span></p><h1 id=\""CigeI\""><span class=\""ne-text\"">改进</span></h1><p id=\""uc426fe61\"" class=\""ne-p\""><span class=\""ne-text\"">因为自己对微信的消息机制又了解得更多了，所以直接在 Keycloak 里对接了微信的消息接口，这样就可以进一步简化开发者的接入了。但同时，老的转发方式也还在，也就是变成了高级接入方式。通过高级接入，开发者可以基于微信的消息做更多的事情。</span></p><p id=\""ue02ced03\"" class=\""ne-p\""><span class=\""ne-text\"">通过使用新版本 [0.5.13](</span><a href=\""https://github.com/Jeff-Tian/keycloak-services-social-weixin/releases/tag/0.5.13\"" data-href=\""https://github.com/Jeff-Tian/keycloak-services-social-weixin/releases/tag/0.5.13\"" target=\""_blank\"" class=\""ne-link\""><span class=\""ne-text\"">https://github.com/Jeff-Tian/keycloak-services-social-weixin/releases/tag/0.5.13</span></a><span class=\""ne-text\""> ) 中，就可以省去自己开发微信消息接口的步骤，直接配置 Keycloak 插件里自带的，即：</span><code class=\""ne-code\""><span class=\""ne-text\"">/realms/&lt;realm&gt;/QrCodeResourceProviderFactory/message</span></code><span class=\""ne-text\"">接口。比如我在体验链接实例里，就是这样配置的：</span></p><p id=\""uaff9d551\"" class=\""ne-p\""><img src=\""https://cdn.nlark.com/yuque/0/2024/png/221736/1706504624416-0baa49a0-9b35-4417-b3c1-2b27a284227b.png\"" width=\""1113\"" id=\""u0e228d0e\"" class=\""ne-image\""></p><h1 id=\""hTp9D\""><span class=\""ne-text\"">待办事项 </span></h1><p id=\""u0473ad1a\"" class=\""ne-p\""><span class=\""ne-text\"">不过，这个关注公众号即登录，还有更多值得优化的地方，比如需要支持分布式缓存等。现在这个插件，只能用在单个 Keycloak 实例中，如果要横向扩展，就需要引入 Redis 或者其他数据库来对扫码状态做存储。</span></p></div>"",
  ""word_count"": 1319,
  ""likes_count"": 0,
  ""comments_count"": 0,
  ""created_at"": ""2024-01-25T10:34:32.000Z"",
  ""updated_at"": ""2024-01-29T05:06:35.000Z"",
  ""deleted_at"": null,
  ""published_at"": ""2024-01-29T05:06:35.000Z"",
  ""first_published_at"": ""2024-01-29T05:06:34.957Z"",
  ""content_updated_at"": ""2024-01-29T05:06:35.000Z"",
  ""user"": {
    ""id"": 221736,
    ""type"": ""User"",
    ""login"": ""tian-jie"",
    ""name"": ""田杰"",
    ""avatar_url"": ""https://cdn.nlark.com/yuque/0/2018/jpeg/anonymous/1544252492004-172d6d7e-350c-47b4-91ea-41e8c04b09ae.jpeg"",
    ""books_count"": 8,
    ""public_books_count"": 3,
    ""followers_count"": 1,
    ""following_count"": 5,
    ""public"": 1,
    ""description"": null,
    ""created_at"": ""2018-12-08T07:01:52.000Z"",
    ""updated_at"": ""2024-01-25T15:05:37.000Z"",
    ""work_id"": """",
    ""_serializer"": ""v2.user""
  },
  ""book"": {
    ""id"": 458374,
    ""type"": ""Book"",
    ""slug"": ""blog"",
    ""name"": ""blog"",
    ""user_id"": 221736,
    ""description"": ""blog"",
    ""creator_id"": 221736,
    ""public"": 1,
    ""items_count"": 283,
    ""likes_count"": 0,
    ""watches_count"": 1,
    ""content_updated_at"": ""2024-01-29T05:06:35.185Z"",
    ""created_at"": ""2019-09-12T11:02:20.000Z"",
    ""updated_at"": ""2024-01-29T05:06:35.000Z"",
    ""user"": null,
    ""_serializer"": ""v2.book""
  },
  ""tags"": [],
  ""path"": ""tian-jie/blog/azl1phhstd3euu1w"",
  ""_serializer"": ""webhook.doc_detail"",
  ""publish"": true,
  ""actor_id"": 221736,
  ""actor"": {
    ""id"": 221736,
    ""type"": ""User"",
    ""login"": ""tian-jie"",
    ""name"": ""田杰"",
    ""avatar_url"": ""https://cdn.nlark.com/yuque/0/2018/jpeg/anonymous/1544252492004-172d6d7e-350c-47b4-91ea-41e8c04b09ae.jpeg"",
    ""books_count"": 8,
    ""public_books_count"": 3,
    ""followers_count"": 1,
    ""following_count"": 5,
    ""public"": 1,
    ""description"": null,
    ""created_at"": ""2018-12-08T07:01:52.000Z"",
    ""updated_at"": ""2024-01-25T15:05:37.000Z"",
    ""work_id"": """",
    ""_serializer"": ""v2.user""
  },
  ""webhook_subject_type"": ""publish"",
  ""action_type"": ""publish"",
  ""url"": ""https://www.yuque.com/tian-jie/blog/azl1phhstd3euu1w""
}}";
}

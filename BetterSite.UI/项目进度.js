//site 页面
//site 页面 标签处理已完成，等待点击site的卡片上保存按钮时，把标签和站点的关系存到sitetag表，
//然后是编辑

//已把标签和站点的关系存到sitetag表，
//该做编辑了

//site页面增加 编辑已做完 
///待做：删除需要完善，删除站点同时要删除标签关系

///增删改 已完成，该做查询和排序了

///站点导入功能基本完成，修改了一些bug  该做查询和排序了

///2015 - 10 - 18
//首页 站长推荐:显示置顶的网站  SiteIsTop = true
//首页 常用站点：显示推到首页的网站 SiteIsHome = true;
//所有站点排序为 SiteAddDate 降序

//2017-01-24
//首页的各分类数据支持自定义排序
//SiteOrderNumber 排序号字段
{///*
    //<div class="row">
    //       <div class="col-md-12">
    //           <table class="table table-condensed ">
    //               <thead>
    //                   <tr>
    //                       <th><h4>最新收录</h4></th>
    //                   </tr>
    //               </thead>
    //               <tbody>
    //                   @foreach (var item in ViewBag.New)
    //{
    //                       <tr>
    //                           <td>
    //                               <h5 class="site-add-date">@item.SiteAddDate</h5>
    //                               <h4>

    //                                   <a href="/Sites/@item.SiteCode" target="_self" title="@item.SiteUrl">@item.SiteName</a>
    //                                   <br /><small>@item.SiteUrl</small>

    //                               </h4>
    //                               <p>
    //                                   @item.SiteProfile
    //                               </p>

    //                           </td>

    //                       </tr>

    //}
    //               </tbody>
    //               <tfoot><tr><td><small style="float:right"><a href="/Sites">更多</a></small></td></tr></tfoot>
    //           </table>
    //       </div>
    //   </div>
    ///*
} 首页的备份


//2017 - 05 - 27
//1首页  图标问题
//2列表页
//  列表左空白有点小
//  站点描述有的太长     标签可以靠上点
//  3数据问题 站点打标签 分类等

//  4详情页
//  图片有点大


//  2017-06－06
//typecode 长度必须大于等于5，关系到活动菜单样式；请用小写
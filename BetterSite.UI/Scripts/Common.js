///*********************************命名空间****************************************/

////声明一个全局对象Namespace，用来注册命名空间
//Namespace = {};
///**
//*   级别：Public
//*   类型：Function
//*   功能：全局对象仅仅存在register函数，参数为名称空间全路径，如"Pansoft.Ctrl"
//*   参数：fullNS：控件名
//*   返回：
//*/
//Namespace.register = function (fullNS) {
//    // 将命名空间切成N部分, 比如Pansoft、Ctrl等
//    var nsArray = fullNS.split('.');
//    var sEval = "";
//    var sNS = "";
//    for (var i = 0; i < nsArray.length; i++) {
//        if (i != 0)
//            sNS += "[\"" + nsArray[i] + "\"]";
//        else
//            sNS += nsArray[i];

//        // 依次创建构造命名空间对象（假如不存在的话）的语句
//        // 比如先创建TBM，然后创建TBM.Ctrl，依次下去
//        sEval += "if (typeof(" + sNS + ") == 'undefined') " + sNS + " = {};"
//    }
//    if (sEval != "") eval(sEval);
//};

////注册命名空间
//Namespace.register("TBM");//TBM 命名空间类
//Namespace.register("TBM.Ctrl");//所有控件包含在TBM.Ctrl 命名空间中。
//Namespace.register("TBM.Data");//数据操作命名空间。
//Namespace.register("TBM.Validate");//验证命名空间。

/*********************************全局公共类****************************************/
var Global = {
    method: {
        //调用后台页面的方法名称
        //查询数据的后台调用方法;
        methodRetrieveEntity: "TBM.DictMgr.DataEntityMgr.SaveData"
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：产生全局唯一guid
    *   参数：
    *   返回：返回guid
    */
    createGuid: function () {
        //var uid="uid"+(new Date()).getTime()+parseInt(Math.random()*100000);
        //return uid;

        var guid = "";
        for (var i = 1; i <= 32; i++) {
            var n = Math.floor(Math.random() * 16.0).toString(16);
            guid += n;
            if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                guid += "-";
        }
        return guid;
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：校验是否全由数字组成
    *   参数：str：输入的字符串
    *   返回：返回bool类型
    */
    isDigit: function (str) {
        var patrn = /^[0-9]{1,20}$/;
        if (!patrn.exec(str)) return false;
        return true;
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：校验是否全由字符组成
    *   参数：str：输入的字符串
    *   返回：返回bool类型
    */
    isChar: function (str) {
        var patrn = /^[a-z]{1,20}$/i;
        if (!patrn.exec(str)) return false;
        return true;
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：舍入函数
    *   参数：length：小数位长度
    *   返回：数值
    */
    round: function (val, precision) {
        var num = Math.pow(10, precision);
        return Math.round(val * num) / num;
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：通过值获取其类型
    *   参数：obj：传入的值
    *   返回：值的类型
    */
    getClass: function (obj) {
        if (obj == null)
            return null;

        var str = null;
        try {
            str = obj.constructor.toString();
            str = str.substring(str.indexOf(" ") + 1, str.indexOf("()"));
        }
        catch (e) {
            str = Object.prototype.toString.call(obj);
            str = str.substring(str.indexOf(" ") + 1, str.indexOf("]"));
        }

        return str;
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：通过对象全名获取对象
    *   参数：name：对象全名，
    *   返回：获取的对象
    */
    getObj: function (name) {
        if (!name) return;

        name = '["' + name.replace(/[[]/g, '.').replace(/[]]/g, '').replace(/[.]/g, '"]["') + '"]';
        try {
            var obj = eval("window" + name);
        } catch (ex) {
            return null;
        }
        return obj;
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：通过对象 在对象下创建相应的属性
    *   参数：name：对象全名，
    *   返回：获取的对象
    */
    setObj: function (obj, name, value) {
        // 将切成N部分, 比如TBM、Ctrl等
        var objArray = name.split('.');
        var sEval = "";
        var sObj = "";
        for (var i = 0; i < objArray.length; i++) {
            if (i == objArray.length - 1) {
                //最后一个值，直接赋值
                obj[objArray[i]] = value;

                return value;
            }
            else {
                if (objArray[i].include("[")) {

                    if (eval("obj." + objArray[i] + "=== undefined || obj." + objArray[i] + " === null")) {
                        eval("obj." + objArray[i] + " = {}");
                    }

                    eval("obj = obj." + objArray[i]);
                }
                else {
                    //中间的对象，如果为空则创建对象
                    if (obj[objArray[i]] === undefined || obj[objArray[i]] === null) {
                        obj[objArray[i]] = {};
                    }

                    obj = obj[objArray[i]];
                }
            }
        }
    },

    /*
        array：数组或数字
        func：循环的方法
        object：绑定的对象，默认是window
        order：顺序还是逆序遍历
    */
    each: function (array, func, object, order) {
        order = order === undefined ? true : order;
        var isbreak;
        if (typeof (array) == "string") return;
        if (array && array.length && array.length > 0 && typeof (array) != "string") {
            if (order) {
                for (var i = 0, len = array.length; i < len; i++) {
                    if (object == "each") {
                        isbreak = func.apply(array[i], [i])
                    } else {
                        isbreak = func.apply(object || window, [array[i], i])
                    }
                    if (isbreak == "$break") break;
                }
            } else {
                for (var len = array.length, i = len - 1; i > -1; i--) {
                    if (object == "each") {
                        isbreak = func.apply(array[i], [i])
                    } else {
                        isbreak = func.apply(object || window, [array[i], i])
                    }
                    if (isbreak == "$break") break;
                }
            }
        } else if (typeof (array) == "number") {
            if (order) {
                for (var i = 0, len = array; i < len; i++) {
                    isbreak = func.apply(object || window, [i])
                    if (isbreak == "$break") break;
                }
            } else {
                for (var len = array, i = len - 1; i > -1; i--) {
                    isbreak = func.apply(object || window, [i])
                    if (isbreak == "$break") break;
                }
            }
        } else if (array && typeof (array) == "object" && "from" in array && "to" in array) {
            if (order) {
                for (var i = array.from, len = array.to; i <= len; i++) {
                    isbreak = func.apply(object || window, [i])
                    if (isbreak == "$break") break;
                }
            } else {
                for (var len = array.to, i = len; i >= array.from; i--) {
                    isbreak = func.apply(object || window, [i])
                    if (isbreak == "$break") break;
                }
            }
        }
    },

    /*
        strObj: "A.B[ind]['C'].Fun(bb).ggg" / "A.B['" + ind + "']['C'].Fun('" + bb +"').ggg"（符合js语法的字符串或把变量拼接成字符串）"A.B['" + ind + "']['C'].Fun('" + bb +"').ggg"
        value: 非空表示没有值则赋值的值（中间若是空，则赋空对象填充），空表示验证是否存在，如果是数组，还验证长度是否是0
        topObj: 表示顶对象
        如果strObj是符合js语法的串（没有拼接），如："A.B[ind]['C'].Fun(bb).ggg"，
                则在前3个参数后面按存在变量的顺序依次传入，完整形式如："A.B[ind]['C'].Fun(bb).ggg", {}, A, vind, vbb
    */
    parseObj: function (strObj, value, topObj) {
        strObj = strObj.replace(/\'/g, "\"").replace(/\[\"/g, "[").replace(/\"]/g, "").replace(/]/g, "")
        var indA = strObj.indexOf(".");
        var indB = strObj.indexOf("[");
        var proArray = [];
        var tcount, count;
        tcount = count = 0;
        var str;
        var strArray = [];
        var args = Array.from(arguments);
        args.shift();
        var flag = false;
        var value = args.shift();
        topObj = args.shift();
        if (value || value === 0 || value === "0") {
            flag = true;
        }

        var foreach = function (proArray, isFirst) {
            var str, funind, bakeObj;
            var strArray = new Array();
            tcount += proArray.length;
            for (var i = 0, len = proArray.length; i < len; i++) {
                if (proArray[i].indexOf("[") != -1) {
                    strArray = proArray[i].split("[");

                    if (strArray.length > 0) {
                        tcount--;
                        foreach(strArray);
                        continue;
                    }
                }

                if (proArray[i].indexOf(".") != -1) {
                    strArray = proArray[i].split(".");

                    if (strArray.length > 0) {
                        tcount--;
                        foreach(strArray);
                        continue;
                    }
                }

                if (topObj == undefined) {
                    if (flag && tcount === count) {
                        topObj = value;
                        continue;
                    } else if (flag) {
                        topObj = {}
                        continue;
                    }

                    topObj = undefined;
                    break;
                }

                count++;

                if (topObj[proArray[i]] == undefined) {
                    if (funind = proArray[i].indexOf("(") != -1) {
                        bakeObj = undefined;
                        try {
                            eval('bakeObj = topObj.' + proArray[i]);
                        } catch (ex) {
                            if (args.length > 0) {
                                proArray[i] = proArray[i].substr(0, funind) + "('" + args[0] + "')";
                                try {
                                    eval("bakeObj = topObj." + proArray[i]);
                                } catch (ex) { }
                                args.shift();
                            }
                        }
                        if (bakeObj != undefined) {
                            topObj = bakeObj;
                            continue;
                        }
                    } else {
                        if (args.length > 0) {
                            proArray[i] = args[0];

                            if (topObj[proArray[i]] != undefined) {
                                topObj = topObj[proArray[i]];
                                args.shift();
                                continue;
                            }
                        }
                    }
                    if (flag && tcount === count) {
                        topObj = topObj[proArray[i]] = value;
                        continue;
                    }
                    if (flag) {
                        topObj = topObj[proArray[i]] = {};
                        continue;
                    }
                    topObj = undefined;
                    break;
                }
                topObj = topObj[proArray[i]];
            }
        }

        if ((indA < indB && indA != -1) || (indA > indB && indB == -1)) {
            proArray = strObj.split(".");
            proArray.shift();
            foreach(proArray, true);
        } else if ((indB < indA && indB != -1) || (indB > indA && indA == -1)) {
            if (strObj.indexOf("[") != -1) {
                strArray = strObj.split("[");
                strArray.shift();
                if (strArray.length > 0) {
                    foreach(strArray, true);
                }
            }
        }

        if (Object.isArray(topObj) && !flag) {//验证
            return topObj.length > 0 ? topObj : undefined;
        }
        return topObj ? topObj : undefined;

    },

    /*
        strObj: "A.B[ind]['C'].Fun(bb).ggg" / "A.B['" + ind + "']['C'].Fun('" + bb +"').ggg"（符合js语法的字符串或把变量拼接成字符串）"A.B['" + ind + "']['C'].Fun('" + bb +"').ggg"
        value: 要给目标对象赋的值
        topObj: 表示顶对象
        如果strObj是符合js语法的串（没有拼接），如："A.B[ind]['C'].Fun(bb).ggg"，
                则在前3个参数后面按存在变量的顺序依次传入，完整形式如："A.B[ind]['C'].Fun(bb).ggg", {}, A, vind, vbb
    */
    setObjVal: function (strObj, value, topObj) {
        strObj = strObj.replace(/\'/g, "\"").replace(/\[\"/g, "[").replace(/\"]/g, "").replace(/]/g, "")
        var indA = strObj.indexOf(".");
        var indB = strObj.indexOf("[");
        var proArray = [];
        var tcount, count;
        tcount = count = 0;
        var str;
        var strArray = [];
        var args = Array.from(arguments);
        args.shift();
        var flag = false;
        var value = args.shift();
        topObj = args.shift();
        if (value || value === 0 || value === "0") {
            flag = true;
        }

        var foreach = function (proArray, isFirst) {
            var str, funind, bakeObj;
            var strArray = new Array();
            tcount += proArray.length;
            for (var i = 0, len = proArray.length; i < len; i++) {
                if (proArray[i].indexOf("[") != -1) {
                    strArray = proArray[i].split("[");

                    if (strArray.length > 0) {
                        tcount--;
                        foreach(strArray);
                        continue;
                    }
                }

                if (proArray[i].indexOf(".") != -1) {
                    strArray = proArray[i].split(".");

                    if (strArray.length > 0) {
                        tcount--;
                        foreach(strArray);
                        continue;
                    }
                }

                if (topObj == undefined) {
                    if (flag && tcount === count) {
                        topObj = value;
                        continue;
                    } else if (flag) {
                        return;
                    }

                    topObj = undefined;
                    break;
                }

                count++;

                if (topObj[proArray[i]] == undefined) {
                    if (funind = proArray[i].indexOf("(") != -1) {
                        bakeObj = undefined;
                        try {
                            eval('bakeObj = topObj.' + proArray[i]);
                        } catch (ex) {
                            if (args.length > 0) {
                                proArray[i] = proArray[i].substr(0, funind) + "('" + args[0] + "')";
                                try {
                                    eval("bakeObj = topObj." + proArray[i]);
                                } catch (ex) { }
                                args.shift();
                            }
                        }
                        if (bakeObj != undefined) {
                            topObj = bakeObj;
                            continue;
                        }
                    } else {
                        if (args.length > 0) {
                            proArray[i] = args[0];

                            if (topObj[proArray[i]] != undefined) {
                                topObj = topObj[proArray[i]];
                                args.shift();
                                continue;
                            }
                        }
                    }
                    if (flag && tcount === count) {
                        topObj = topObj[proArray[i]] = value;
                        continue;
                    }
                    if (flag) {
                        return;
                    }
                    topObj = undefined;
                    break;
                }

                if (tcount == count) {
                    topObj = topObj[proArray[i]] = value;
                } else {
                    topObj = topObj[proArray[i]];
                }
            }
        }

        if ((indA < indB && indA != -1) || (indA > indB && indB == -1)) {
            proArray = strObj.split(".");
            proArray.shift();
            foreach(proArray, true);
        } else if ((indB < indA && indB != -1) || (indB > indA && indA == -1)) {
            if (strObj.indexOf("[") != -1) {
                strArray = strObj.split("[");
                strArray.shift();
                if (strArray.length > 0) {
                    foreach(strArray, true);
                }
            }
        }
    },

    /*
        strXML: 要转化成xml的字符串
        head: 是否包含'<?xml?>节点
    */
    stringToXml: function (strXML, head) {
        var xmlDoc;
        if (head) {
            strXML = '<?xml version="1.0" encoding="UTF-8"?>' + strXML || "";
        } else {
            if (strXML) {
                strXML = strXML.replace(/\<\?xml.*\?\>/gi, "");
            }
        }
        if (window.ActiveXObject) { // IE
            try {
                xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
                if (!xmlDoc) xmlDoc = new ActiveXObject("MSXML2.DOMDocument.3.0");
                xmlDoc.loadXML(strXML);
            } catch (e) { }
        }
        else if (window.XMLHttpRequest) { //firefox
            var oParser = new DOMParser();
            xmlDoc = oParser.parseFromString(strXML, "text/xml");
            if (xmlDoc.documentElement.tagName == "parsererror") {
                var oSerializer = new XMLSerializer();
                alert("An error occurred:\n错误代码: " + oSerializer.serializeToString(oXmlDom.documentElement));
            }
        }
        return xmlDoc;
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：格式化xml
    *   参数：xml：需格式化的xml串
    *   返回：格式化后的xml
    */
    formatXml: function (xml) {
        xml = xml.replace(/[&]/g, "&amp;");
        xml = xml.replace(/[<]/g, "&lt;");
        xml = xml.replace(/[>]/g, "&gt;");
        xml = xml.replace(/[']/g, "&apos;");
        xml = xml.replace(/["]/g, "&quot;");
        return xml;
    },

    //jsonToXml
    //[{"PluginsID":"PanERP.DictMgr.DataEntityMgr.SaveData","ID":"pageID","Type":"string","OrderNo":"0","State":"0"},
    // {"PluginsID":"PanERP.DictMgr.DataEntityMgr.SaveData","ID":"data","Type":"object","OrderNo":"1","State":"0"}]    
    //<item paramid='dataStatusForNoPass' data_type='int' desc='AAA'></row>
    modelDataToXml: function (datas, root, itemName) {
        var xml = new StringBuilder();
        root = root || "Data"
        xml.append("<" + root + ">");
        this.each(datas, function (data, ind) {
            var item = itemName || "item"
            xml.append("<" + item);
            for (var p in data) {
                if ((itemName && p == itemName) || p == "Status" || p == "RowNo")
                    continue;
                xml.append(' ' + p + '="' + data[p] + '"');
            }
            xml.append("></" + item + ">");
        }, this)
        xml.append("</" + root + ">");
        return xml.toString();
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：深层复制对象
    *   参数：obj：对象
    *   返回：与源对象相同的新对象
    */
    fullclone: function (obj, key) {
        var newobj = null,
            child,
            type = Global.getClass(obj);

        switch (type) {
            case "Object":
                newobj = {};
                for (var _key in obj) {
                    child = obj[_key];
                    newobj[_key] = this.fullclone(child, _key);
                }
                break;
            case "Array":
                newobj = [];
                //克隆数组的值                  
                for (var i = 0; i < obj.length; i++) {
                    child = obj[i];
                    newobj.push(this.fullclone(child));
                }
                break;
            default:
                newobj = obj;
                break;
        }
        child = null;
        type = null;

        return newobj;
    },

    //销毁处理
    dispose: function (obj, key) {
        var child,
            type = Global.getClass(obj);

        switch (type) {
            case "klass":
            case "Object":
                for (var _key in obj) {
                    child = obj[_key];
                    this.dispose(child, _key);
                }
                break;
            case "Array":
                for (var i = 0; i < obj.length; i++) {
                    child = obj[i];
                    this.dispose(child);
                }
                break;
        }

        //销毁对象
        obj = null;
        child = null;
        type = null;
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：调用方法获取返回值
    *   参数：cate：方法集合标示
    *         command：方法标示
    *         dataType：返回值类型 xml/html/script/json/jsonp/text
    *         param：函数的参数值，以json格式传入 {user:"1"}
    *         callback：回调函数
    *         async：是否异步处理
    *   返回：返回值
    */
    ajax: function (command, dataType, param, callback, err, async, cate) {
        var url = methodCenterPath;
        var data = "";
        var contentType = "application/x-www-form-urlencoded;charset=UTF-8";
        //转换参数
        var results = [];

        //返回json格式的值
        if (dataType == "json") {
            for (var key in param) {
                results.push("\\\"" + key + "\\\":\\\"" + param[key] + "\\\"");
            }
            contentType = "application/json;charset=UTF-8";

            data = "{methodfullName:\"" + command + "\",paramStr:\"{" + results.join(',') + "\"}}";
        } else {
            data = { methodfullName: command, paramStr: Object.toJSON(param) };
        }

        //这时的results 作为ajax  是否执行成功的判断的返回值，默认为执行成功
        results = true;

        //如果async 没有定义，则为同步
        if (async == undefined) {
            async = true;
        }

        //获取服务器端的json串
        jQuery.ajax({
            type: "POST",                                                       //访问WebService使用Post方式请求
            contentType: contentType,                                           //WebService 会返回Json类型
            async: async,
            url: url,                                                           //调用WebService的地址和方法名称组合---WsURL/方法名
            data: data,                                                          //这里是要传递的参数，格式为 data: "{paraName:paraValue}",下面将会看到      
            dataType: dataType,
            success: callback,
            error: function (xhr, msg, e) {
                var str = xhr.responseText;
                var idx = str.indexOf("@@");
                if (str && idx > -1) {
                    str = str.split("@@");
                    str = str[1];
                }

                if (str && str.length > 0) {
                    if (err)
                        err(str);
                    else {
                        //alert(str);
                        Cookie.setCookie("errorDetail", str, 1);
                        var href = Url.getWebAppPath() + '/Error.html';
                        if (jQuery('#ErrDia').length == 0) {
                            jQuery(document.createElement('div'))
                                .attr({ id: 'ErrDia' })
                                .dialog({
                                    title: '错误',
                                    width: 300,
                                    height: 250,
                                    closed: false,
                                    cache: false,
                                    href: href,
                                    modal: true
                                });
                        }
                        else {
                            jQuery('#Detail').text(Cookie.getCookie("errorDetail"));
                            jQuery('#ErrDia').dialog('open');
                        }
                    }
                }
                results = false;
            }
        });

        return results;
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：调用方法获取返回值
    *   参数：cate：方法集合标示
    *         command：方法标示
    *         param：函数的参数值，以json格式传入 {user:"1"}
    *         callback：回调函数
    *         async：是否异步处理
    *   返回：返回文本格式的值
    */
    getText: function (command, param, callback, async, cate, err) {
        return this.ajax(command, "xml", param,
        function (ret) {
            if (!callback)
                return;

            //如果为FF浏览器，则取值不同。
            callback(ret.text ? ret.text : ret.childNodes[0].textContent);
            //if (jQuery.browser.msie && jQuery.browser.version < 9)
            //{
            //    callback(ret.text);                
            //}
            //else
            //{
            //    callback(ret.childNodes[0].textContent);
            //}
            ret = null;
        }, err, async, cate);
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：调用方法获取返回值
    *   参数：cate：方法集合标示
    *         command：方法标示
    *         param：函数的参数值，以json格式传入 {user:"1"}
    *         callback：回调函数
    *         async：是否异步处理
    *   返回：返回JSON格式的值
    */
    getJson: function (command, param, callback, async, cate, err) {
        return this.ajax(command, "xml", param,
        function (ret) {
            if (!callback)
                return;

            //如果为FF浏览器，则取值不同。
            callback(ret.text ? ret.text.evalJSON() : (ret.childNodes[0].textContent ? ret.childNodes[0].textContent.evalJSON() : ret.childNodes[0].textContent));
            //if (jQuery.browser.msie && jQuery.browser.version < 9)            
            //{
            //    callback(ret.text.evalJSON());
            //}
            //else
            //{
            //    callback(ret.childNodes[0].textContent.evalJSON());
            //}
            ret = null;
        }, err, async, cate);
    },
    //功能：调用方法无返回值
    CallMethod: function (command, param, callback, async, cate, err) {
        return this.ajax(command, "xml", param,
        function (ret) {
            if (!callback)
                return;

            callback();

            ret = null;
        }, err, async, cate);
    },
    /**
    *   级别：Public
    *   类型：Function
    *   功能：调用方法获取返回值
    *   参数：cate：方法集合标示
    *         command：方法标示
    *         param：函数的参数值，以json格式传入 {user:"1"}
    *         callback：回调函数
    *   返回：返回XML格式的值
    */
    getXml: function (command, param, callback, async, cate, err) {
        return this.ajax(command, "xml", param,
        function (ret) {
            callback(ret.text);
            ret = null;
        }, err, async, cate);
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：调用服务获取json对象
    *   参数：obj：待转换的对象
    *          rule：转换规则{s1:"t1";s2:"t2"}
    *   返回：与源对象相同的新对象
    */
    adapters: function (objs, rule) {
        var targetOjbs = [];
        for (var i = 0; i < objs.length; i++) {
            targetOjbs.push(this.adapter(objs[i], rule));
        }
        return targetOjbs;
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：调用服务获取json对象
    *   参数：obj：待转换的对象
    *          rule：转换规则{s1:"t1";s2:"t2"}
    *   返回：与源对象相同的新对象
    */
    adapter: function (obj, rule) {
        //要转换的目标对象
        var targetObj = {};
        var targetName;
        for (var name in rule) {
            targetName = rule[name];
            targetObj[targetName] = obj[name];
        }

        return targetObj;
    },

    DateAdd: function (strInterval, NumDay, dtDate) {
        var dtTmp = new Date(dtDate);
        if (isNaN(dtTmp))
            dtTmp = new Date();
        switch (strInterval) {
            case "s":
                return new Date(Date.parse(dtTmp) + (1000 * NumDay));
            case "n":
                return new Date(Date.parse(dtTmp) + (60000 * NumDay));
            case "h":
                return new Date(Date.parse(dtTmp) + (3600000 * NumDay));
            case "d":
                return new Date(Date.parse(dtTmp) + (86400000 * NumDay));
            case "w":
                return new Date(Date.parse(dtTmp) + ((86400000 * 7) * NumDay));
            case "m":
                return new Date(dtTmp.getFullYear(), (dtTmp.getMonth())
								+ NumDay, dtTmp.getDate(), dtTmp.getHours(),
						dtTmp.getMinutes(), dtTmp.getSeconds());
            case "y":
                return new Date((dtTmp.getFullYear() + NumDay), dtTmp
								.getMonth(), dtTmp.getDate(), dtTmp.getHours(),
						dtTmp.getMinutes(), dtTmp.getSeconds());
        }
    },

    //获取HTML版本
    getIEVersion: function () {
        var version = navigator.appVersion;
        //var start = version.indexOf("MSIE");
        //version = version.slice(start + 5, start + 6);
        version = version.split(".")[0];
        return parseInt(version);
    }
};

var virtualPath = undefined;
var webappPath = undefined;
/*********************************虚拟目录相关处理****************************************/
var Url = {
    /**
    *   级别：Privatte
    *   类型：Var
    *   功能：存放参数　Dictionary
    */
    g_Params: null,
    /**
    *   级别：Public
    *   类型：Function
    *   功能：得到应用目录
    *   参数：无
    *   返回：应用目录
    */
    getWebAppPath: function () {
        if (webappPath == undefined)
            webappPath = "http://" + window.location.hostname + "/" + this.getVirtualPath();
        return webappPath;
    },
    ///**
    //*   级别：Public
    //*   类型：Function
    //*   功能：得到虚拟目录
    //*   参数：无
    //*   返回：虚拟目录
    //*/
    //getVirtualPath: function () {
    //    if (virtualPath == undefined) {
    //        var p = window.location.pathname;
    //        if (p.substring(0, 1) != "/")
    //            p = "/" + p;
    //        var str = p.replace(/^[/]\w*[/]/, "");			//将虚拟目录从字符串中清除
    //        virtualPath = p.substring(0, p.length - str.length);	//返回虚拟目录
    //    }
    //    return virtualPath;
    //},

    /**
    *   级别：Private
    *   类型：Function
    *   功能：得到URL参数列表
    *   参数：vs_pms：参数串　param1=1&param2=2
    *   返回：Dictionary
    */
    _getParams: function (vs_pms) {
        //var pms = new Hash(); 
        var pms = {};
        if (vs_pms == null || vs_pms == undefined || vs_pms.length == 0) return pms;
        if (vs_pms.substring(0, 1) == "?") vs_pms = vs_pms.substr(1);
        var va_pms = vs_pms.split('&');
        for (var i = 0; i < va_pms.length; i++) {
            var subP = va_pms[i].split('=');
            if (subP.length == 0) continue;
            if (subP.length > 1) {
                //pms.set(subP[0].toLowerCase(), subP[1]) ;
                pms[subP[0].toLowerCase()] = subP[1];
            }
            else {
                //pms.set(subP[0], "") ;
                pms[subP[0]] = "";
            }
        }
        return pms;
    },

    /**
    *   级别：Private
    *   类型：Function
    *   功能：得到某一个参数值
    *   参数：vs_pms：参数串　param1=1&param2=2
    *   参数：name：参数名称  param1
    *   返回：参数值，未找到返回null
    */
    _getParam: function (vs_pms, name) {
        if (name == null || name == undefined || name.length == 0) return null;
        name = name.toLowerCase();
        if (this.g_Params == null) this.g_Params = this._getParams(vs_pms);
        if (this.g_Params == null) return null;

        //return this.g_Params.get(name);
        return this.g_Params[name];
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：得到URL参数列表
    *   参数：无
    *   返回：Dictionary
    */
    getParams: function () {
        return this._getParams(document.location.search);
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：得到URL参数列表
    *   参数：name：参数名称  param1
    *   返回：数值
    */
    getParam: function (name) {
        return this._getParam(document.location.search, name);
    },

    /**
    *   级别：Public
    *   类型：Function
    *   功能：格式化URL
    *   参数：urlstr：参数名称  param1
    *   返回：格式化后的url
    */
    formatUrl: function (urlstr) {
        if (urlstr == null)
            return;
        return urlstr;
    }
};

var Convert = {
    NumToString: function (num) {
        //转换之前的科学计数法表示  
        var valStr = new String(num);
        var retVal = valStr;
        var arr = valStr.split('e');
        if (arr.length > 1) {
            var value = arr[0];
            var pow = arr[1];
            if (pow > 0)
                retVal = value * Math.pow(10, pow);
            else {
                arr = value.split('.');
                if (arr.length > 1)
                    pow -= arr[1].length;
                retVal = String(num * Math.pow(10, -pow));
                retVal = "0." + retVal.leftPad(-pow, '0');
            }
        }
        return retVal;
    }
};

/*********************************XML 转 JSON 工具****************************************/
var XmltoJson = {
    parser: function (xmlcode, ignoretags, debug) {
        if (!ignoretags) { ignoretags = "" };
        xmlcode = xmlcode.replace(/\s*\/>/g, '/>');
        xmlcode = xmlcode.replace(/<\?[^>]*>/g, "").replace(/<\![^>]*>/g, "");
        if (!ignoretags.sort) { ignoretags = ignoretags.split(",") };
        var x = this.no_fast_endings(xmlcode);
        x = this.attris_to_tags(x);
        x = escape(x);
        x = x.split("%3C").join("<").split("%3E").join(">").split("%3D").join("=").split("%22").join("\"");
        for (var i = 0; i < ignoretags.length; i++) {
            x = x.replace(new RegExp("<" + ignoretags[i] + ">", "g"), "*$**" + ignoretags[i] + "**$*");
            x = x.replace(new RegExp("</" + ignoretags[i] + ">", "g"), "*$***" + ignoretags[i] + "**$*")
        };
        x = '<jsontagwrapper>' + x + '</jsontagwrapper>';
        this.xmlobject = {};
        var y = this.xml_to_object(x).jsontagwrapper;
        if (debug) { y = this.show_json_structure(y, debug) };
        return y
    },
    xml_to_object: function (xmlcode) {
        var x = xmlcode.replace(/<\//g, "�");
        x = x.split("<");
        var y = [];
        var level = 0;
        var opentags = [];
        for (var i = 1; i < x.length; i++) {
            var tagname = x[i].split(">")[0];
            opentags.push(tagname);
            level++
            y.push(level + "<" + x[i].split("�")[0]);
            while (x[i].indexOf("�" + opentags[opentags.length - 1] + ">") >= 0) { level--; opentags.pop() }
        };
        var oldniva = -1;
        this.xmlobject = undefined;//清空this.xmlobject对象
        var objname = "this.xmlobject";
        for (var i = 0; i < y.length; i++) {
            var preeval = "";
            var niva = y[i].split("<")[0];
            var tagnamn = y[i].split("<")[1].split(">")[0];
            var rest = y[i].split(">")[1];
            if (niva <= oldniva) {
                var tabort = oldniva - niva + 1;
                for (var j = 0; j < tabort; j++) { objname = objname.substring(0, objname.lastIndexOf(".")) }
            };
            objname += "." + tagnamn;
            var pobject = objname.substring(0, objname.lastIndexOf("."));
            if (eval("typeof " + pobject) != "object") { preeval += pobject + "={value:" + pobject + "};\n" };
            var objlast = objname.substring(objname.lastIndexOf(".") + 1);
            var already = false;
            for (k in eval(pobject)) { if (k == objlast) { already = true } };
            var onlywhites = true;
            for (var s = 0; s < rest.length; s += 3) {
                if (rest.charAt(s) != "%") { onlywhites = false }
            };
            if (rest != "" && !onlywhites) {
                if (rest / 1 != rest) {
                    rest = "'" + rest.replace(/\'/g, "\\'") + "'";
                    rest = rest.replace(/\*\$\*\*\*/g, "</");
                    rest = rest.replace(/\*\$\*\*/g, "<");
                    rest = rest.replace(/\*\*\$\*/g, ">")
                }
            }
            else { rest = "{}" };
            if (rest.charAt(0) == "'") { rest = 'unescape(' + rest + ')' };
            if (already && !eval(objname + ".sort")) { preeval += objname + "=[" + objname + "];\n" };
            var before = "="; after = "";
            if (already) { before = ".push("; after = ")" };
            var toeval = preeval + objname + before + rest + after;
            eval(toeval);
            if (eval(objname + ".sort")) { objname += "[" + eval(objname + ".length-1") + "]" };
            oldniva = niva
        };
        return this.xmlobject
    },
    show_json_structure: function (obj, debug, l) {
        var x = '';
        if (obj.sort) { x += "[\n" } else { x += "{\n" };
        for (var i in obj) {
            if (!obj.sort) { x += i + ":" };
            if (typeof obj[i] == "object") {
                x += this.show_json_structure(obj[i], false, 1)
            }
            else {
                if (typeof obj[i] == "function") {
                    var v = obj[i] + "";
                    x += v
                }
                else if (typeof obj[i] != "string") { x += obj[i] + ",\n" }
                else { x += "'" + obj[i].replace(/\'/g, "\\'").replace(/\n/g, "\\n").replace(/\t/g, "\\t").replace(/\r/g, "\\r") + "',\n" }
            }
        };
        if (obj.sort) { x += "],\n" } else { x += "},\n" };
        if (!l) {
            x = x.substring(0, x.lastIndexOf(","));
            x = x.replace(new RegExp(",\n}", "g"), "\n}");
            x = x.replace(new RegExp(",\n]", "g"), "\n]");
            var y = x.split("\n"); x = "";
            var lvl = 0;
            for (var i = 0; i < y.length; i++) {
                if (y[i].indexOf("}") >= 0 || y[i].indexOf("]") >= 0) { lvl-- };
                tabs = ""; for (var j = 0; j < lvl; j++) { tabs += "\t" };
                x += tabs + y[i] + "\n";
                if (y[i].indexOf("{") >= 0 || y[i].indexOf("[") >= 0) { lvl++ }
            };
            if (debug == "html") {
                x = x.replace(/</g, "&lt;").replace(/>/g, "&gt;");
                x = x.replace(/\n/g, "<BR>").replace(/\t/g, "&nbsp;&nbsp;&nbsp;&nbsp;")
            };
            if (debug == "compact") { x = x.replace(/\n/g, "").replace(/\t/g, "") }
        };
        return x
    },
    no_fast_endings: function (x) {
        x = x.split("/>");
        for (var i = 1; i < x.length; i++) {
            var t = x[i - 1].substring(x[i - 1].lastIndexOf("<") + 1).split(" ")[0];
            x[i] = "></" + t + ">" + x[i]
        };
        x = x.join("");
        return x
    },
    attris_to_tags: function (x) {
        var d = ' ="\''.split("");
        x = x.split(">");
        for (var i = 0; i < x.length; i++) {
            var temp = x[i].split("<");
            for (var r = 0; r < 4; r++) { temp[0] = temp[0].replace(new RegExp(d[r], "g"), "_jsonconvtemp" + r + "_") };
            if (temp[1]) {
                temp[1] = temp[1].replace(/'/g, '"');
                temp[1] = temp[1].split('"');
                for (var j = 1; j < temp[1].length; j += 2) {
                    for (var r = 0; r < 4; r++) { temp[1][j] = temp[1][j].replace(new RegExp(d[r], "g"), "_jsonconvtemp" + r + "_") }
                };
                temp[1] = temp[1].join('"')
            };
            x[i] = temp.join("<")
        };
        x = x.join(">");
        x = x.replace(/ ([^=]*)=([^ |>]*)/g, "><$1>$2</$1");
        x = x.replace(/>"/g, ">").replace(/"</g, "<");
        for (var r = 0; r < 4; r++) { x = x.replace(new RegExp("_jsonconvtemp" + r + "_", "g"), d[r]) };
        return x
    }
};
/*********************************事件处理****************************************/
// 功能：页面初始时，为对象追加事件
//      obj：追加事件的对象
//      evnt：追加的事件
//      func：事件调用的方法
//      condition：追加事件条件
function attachEvents(obj, evnt, func, condition) {
    if (evnt == null || evnt == "" || func == null || typeof (func) != "function")
        return;

    window.attachEvent("onload", function () {
        if (condition == null || eval(condition)) {
            if (obj == null) {
                obj = document.body;
            } else if (typeof (obj) == "string") {
                obj = document.getElementById(obj);
            }
            obj.attachEvent(evnt, func);
        }
    })
};

// 功能：鼠标悬浮到某种类型的控件时处理
// type：控件类型
// func：调用函数
// 例子：this.style.cursor = "hand";	//显示手形   
//       this.title = this.innerText;   //提示信息同于内容
function attachMouseover(type, func, condition) {
    attachEvents(null, "onmouseover", function () {
        var ele = window.event.srcElement;
        if (type != null && ele.tagName != type)
            return;
        //调用事件，传入当前控件
        func.call(ele);
    }, condition)
};

// 功能：鼠标悬离开某种类型的控件时处理
// type：控件类型
// func：调用函数
// condition：追加事件条件
// 例子：this.style.cursor = "hand";	//显示手形   
//       this.title = this.innerText;   //提示信息同于内容
function attachMouseout(type, func, condition) {
    attachEvents(null, "onmouseout", function () {
        var ele = window.event.srcElement;
        if (type != null && ele.tagName != type)
            return;
        //调用事件，传入当前控件
        func.call(ele);
    }, condition)
};

// 功能：鼠标点击某类型控件后抬起始时处理
// type：控件类型
// func：调用函数
// condition：追加事件条件
// 例子：this.style.cursor = "hand";	//显示手形   
//       this.title = this.innerText;   //提示信息同于内容
function attachMouseUp(type, func, condition) {
    attachEvents(null, "onmouseup", function () {
        var ele = window.event.srcElement;
        if (type != null && ele.tagName != type)
            return;
        //调用事件，传入当前控件
        func.call(ele);
    }, condition)
};

/*********************************字符串累加处理****************************************/
///**
//*   级别：Public
//*   类型：Function
//*   功能：创建字符串累加类
//*   返回：stringBuilder对象
//*/
//StringBuilder = Class.create({
//    initialize: function (cfg) {
//        this.__strings__ = new Array;
//        this.data = this.__strings__;
//    },
//    /**
//    *   级别：Public
//    *   类型：Function
//    *   功能：追加一个累加串
//    *   参数：str：累加的串
//    *   返回：stringBuilder对象
//    */
//    append: function (str) {
//        this.__strings__.push(str);
//    },
//    /**
//    *   级别：Public
//    *   类型：Function
//    *   功能：返回串的结果
//    *   返回：累加后的串
//    */
//    toString: function () {
//        return this.__strings__.join("");
//    }
//});

//Object.extend(Function.prototype, {
//    //第一个参数是方法，以后的是变量，把方法和变量绑定方法的参数中，this不变
//    wrapargs: function () {
//        var __method = this, args = $A(arguments), object = args.shift();
//        return function () {
//            return object.apply(this, [__method.bind(this)].concat(args).concat($A(arguments)));
//        }
//    },
//    //把多个参数绑定到方法参数中，this不变
//    args: function () {
//        var __method = this, args = $A(arguments);
//        return function () {
//            return __method.apply(this, args.concat($A(arguments)));
//        }
//    }
//});

///**
//*   级别：Public
//*   类型：Function
//*   功能：字符串格式
//*   返回：stringBuilder对象
//*/
//Object.extend(String, {
//    //字符串格式化函数，element.innerHTML = String.format(’<a href=”%1″ onclick=”alert(\’%2\’);”>%3</a>’, url, msg, text);
//    format: function (str) {
//        var args = arguments, re = new RegExp("%([1-" + args.length + "])", "g");
//        return String(str).replace(
//            re,
//            function ($1, $2) {
//                return args[$2];
//            }
//        );
//    }
//});

//String.prototype.leftPad = function (n, s) {
//    s = s || " ";
//    if (this.length < n) {
//        var ts = new Array(n - 1);
//        ts[n - 1] = this;
//        for (var i = 0; i < n - this.length; i++) {
//            ts[i] = s;
//        }
//        return ts.join("");
//    } else {
//        return this;
//    }
//}


/////<summary>
///// 对prototype的extend方法的扩展
/////</summary>
/////<param name="destination" type="object">
/////1:目标对象
/////</param>
/////<param name="source" type="object">
/////1:扩展自对象
/////</param>
/////<returns type="object" />
/////用法：和Class.create的用法类似，只是$super改为$base的形式，可以调用扩展之前的方法。
//Object._extend = function (destination, source) {
//    if (!destination.baseClass) {
//        destination.baseClass = {};
//    }
//    for (var property in source) {
//        var uid = Global.createGuid();
//        var value = source[property]
//        if (destination && Object.isFunction(destination[property]) &&
//             Object.isFunction(value) && value.argumentNames().first() == "$super") {
//            destination[uid] = destination.baseClass[property] = destination[property];
//            destination.baseClass[property] = destination.baseClass[property].bind(destination);
//            var method = value;
//            value = (function (m) {
//                return function () { return destination[m].apply(this, arguments) };
//            })(uid).wrap(method);
//        }
//        destination[property] = value;
//    }
//    return destination;
//};

//原始的window.alert 方法 
window.oriAlert = window.alert;
/**
*   级别：Public
*   类型：Function
*   功能：重写window.alert 方法
*/
window.alert = function (msg) {
    if (msg == null || msg == undefined)
        msg = "无消息！";

    jQuery.messager.alert('提示', msg);
};
/**
*   级别：Public
*   类型：Function
*   功能：封装jQuery的messager.alert方法（普通提示）
*/
window.InfoMes = function (msg) {
    if (!msg)
        msg = "无消息！";
    jQuery.messager.alert('提示', msg, 'info');
};
/**
*   级别：Public
*   类型：Function
*   功能：封装jQuery的messager.alert方法（错误提示）
*/
window.ErrorMes = function (msg, func) {
    if (!msg)
        msg = "无消息！";
    jQuery.messager.alert('错误', msg, 'error', func);
};
/**
*   级别：Public
*   类型：Function
*   功能：封装jQuery的messager.alert方法（警告提示）
*/
window.WarnMes = function (msg) {
    if (!msg)
        msg = "无消息！";
    jQuery.messager.alert('警告', msg, 'warning');
};
window.oriConfirm = window.confirm;
/**
*   级别：Public
*   类型：Function
*   功能：window.confirm 方法
*   返回：bool
*/
window.confirm = function (msg, func) {
    if (msg == null || msg == undefined)
        msg = "确认？";

    jQuery.messager.confirm('确认', msg, func);
};

Date.prototype.yyyyMMdd = function () {
    var yyyy = this.getFullYear().toString();
    var MM = (this.getMonth() + 1).toString();
    var dd = this.getDate().toString();
    return yyyy + (MM[1] ? MM : "0" + MM[0]) + (dd[1] ? dd : "0" + dd[0]);
};

/// <summary>
/// 存取Cookie的类
/// </summary>
Cookie = {
    /// <summary>
    /// 添加cookie
    /// </summary>
    /// <param name="c_name">cookie存储的键名称</param>
    /// <param name="value">cookie存储的键值</param>
    /// <param name="expiredays">cookie过期天数</param>
    setCookie: function (c_name, value, expiredays) {
        jQuery.cookie(c_name, null, { path: "/" });
        jQuery.cookie(c_name, value, { path: "/", expires: expiredays });
    },
    /// <summary>
    /// 添加cookie
    /// </summary>
    /// <param name="c_name">cookie存储的键名称</param>
    /// <returns>cookie存储的键值</returns>
    getCookie: function (c_name) {
        return jQuery.cookie(c_name);
    },

    //为了删除指定名称的cookie，可以将其过期时间设定为一个过去的时间
    delCookie: function (c_name) {
        jQuery.cookie(c_name, null, { path: "/" });
    }
};

jQuery.cookie = function (name, value, options) {
    if (typeof value != 'undefined') { // name and value given, set cookie
        options = options || {};
        if (value === null) {
            value = '';
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            } else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString(); // use expires attribute, max-age is not supported by IE
        }
        var path = options.path ? '; path=' + options.path : '';
        var domain = options.domain ? '; domain=' + options.domain : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    } else { // only name given, get cookie
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = jQuery.trim(cookies[i]);
                // Does this cookie string begin with the name we want?
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};

//实体类相关处理
Entity = {
    getDictColInfo: function (entity) {
        //Global.getJson("Sinopec.TBM.Business..GetList", { "condition": this.condition }, function (model) {
        //    this.data = model;
        //}.bind(this), false);
    }
}

//读取cookie文件件
function readCookies() {
    alert(document.cookie);
    alert("userName:" + getCookie("username").toString() + "passWord:" + getCookie("password").toString());
}

//Enum.saveType.Delete
var Enum = {
    //数据类型
    DataType: {
        /// <summary>
        /// 字符串
        /// </summary>
        String: "string",
        /// <summary>
        /// 整形
        /// </summary>
        Integer: "int",
        /// <summary>
        /// 浮点
        /// </summary>
        Float: "float",
        /// <summary>
        /// 日期
        /// </summary>
        Date: "date",
        /// <summary>
        /// 时间类型
        /// </summary>
        Time: "time",
        /// <summary>
        /// 布尔
        /// </summary>
        Boolean: "bool",
        /// <summary>
        /// 引用
        /// </summary>
        Object: "object",
        /// <summary>
        /// 长字符串
        /// </summary>
        Text: "text"
    }
};

//服务中心路径
//var methodCenterPath = Url.getVirtualPath() + "InvokeMethod.asmx/Invoke";

//屏蔽窗体鼠标右键菜单
jQuery(document).bind("contextmenu", function (e) {
    return true;
});
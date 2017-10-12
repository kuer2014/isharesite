$(function () {
    //if ($.cookie('d')) {
    //    var cachewords = $.cookie('d').split(',');
    //    $('#searchhistory').html(cachewords.join());
    //}
    BuildHistory();
    AddNewWord();
    //$("#source").dblclick(Search);
    $("#source").select(Search);
})
// var word = '';

function Search(wordtxt) {
    if (wordtxt) {
        //$("#source").select(function(){

        //word=document.selection.createRange().text;
        if (!(typeof wordtxt == 'string'))
            wordtxt = document.getSelection().toString();

        //word= document.execCommand('Copy');
        word = wordtxt.toLocaleLowerCase().trim();
        if (word && word.length <= 30) {
            //追加单词解释
            BuildMeans(word);
            // 追加单词联想（词组短语等）
            BuildSuggst(word);
            //保存历史
            BuildHistory(word);
        }
    }
};
//追加单词解释
function BuildMeans(word) {
    var wordStrArr = [];
    $.ajax({
        //url: 'http://api.fanyi.baidu.com/api/trans/vip/translate',
        url: 'http://www.iciba.com/index.php?a=getWordMean&c=search&word=' + word,
        type: 'get',
        dataType: 'jsonp',
        data: {
        },
        success: function (data) {
            //   debugger;
            if (data && data.errno == 0) {
                var baseInfo = data.baesInfo;
                if (baseInfo.symbols && baseInfo.symbols.length > 0) {//成功查到

                    var wordtxtatt = [];////追加到生词本备用
                    //1
                    wordStrArr.push('<div><h4>' + baseInfo.word_name + '</h4> </div>');
                    wordtxtatt.push('<b>' + baseInfo.word_name + '</b> ');

                    //2
                    var symbol = baseInfo.symbols[0];
                    wordStrArr.push('<div>英: [<b>' + symbol.ph_en + '</b>] 美: [<b>' + symbol.ph_am + '</b>] </div>');
                    wordtxtatt.push('英: [<b>' + symbol.ph_en + '</b>] 美: [<b>' + symbol.ph_am + '</b>] ');

                    //5解释
                    if (symbol.parts && symbol.parts.length > 0) {
                        for (var i = 0; i < symbol.parts.length; i++) {
                            var part = symbol.parts[i]
                            wordStrArr.push('<div><b>' + part.part + '</b> ' + part.means.toString() + '</div>');
                            wordtxtatt.push('<b>' + part.part + '</b> ' + part.means.toString() + ' ');
                        }
                    }
                    //6时态
                    var exchange = baseInfo.exchange;
                    if (exchange) {
                        wordStrArr.push('<div>');
                        if (exchange.word_past && exchange.word_past.length > 0) {
                            wordStrArr.push('过去式： ' + exchange.word_past);
                            wordStrArr.push('<br/>');
                            wordtxtatt.push('过去式： ' + exchange.word_past);

                        }
                        if (exchange.word_past && exchange.word_past.length > 0) {
                            wordStrArr.push('过去分词： ' + exchange.word_past);
                            wordStrArr.push('<br/>');
                            wordtxtatt.push('过去分词： ' + exchange.word_past);
                        }
                        if (exchange.word_ing && exchange.word_ing.length > 0) {
                            wordStrArr.push('现在分词： ' + exchange.word_ing);
                            wordStrArr.push('<br/>');
                            wordtxtatt.push('现在分词： ' + exchange.word_ing);
                        }
                        if (exchange.word_third && exchange.word_third.length > 0) {
                            wordStrArr.push('第三人称单数： ' + exchange.word_third);
                            wordStrArr.push('<br/>');
                            wordtxtatt.push('第三人称单数： ' + exchange.word_third);
                        }
                        wordStrArr.push('</div>');
                        //wordStrArr.push('<div>过去式： ' + exchange.word_past + ' 过去分词： ' + exchange.word_past + ' 现在分词： ' + exchange.word_ing + ' 第三人称单数： ' + exchange.word_third + '</div>');
                        //wordtxtatt.push('过去式： ' + exchange.word_past + ' 过去分词： ' + exchange.word_past + ' 现在分词： ' + exchange.word_ing + ' 第三人称单数： ' + exchange.word_third + ' ');
                    }
                    //3
                    if (baseInfo.frequence && baseInfo.frequence > 0) {
                        wordStrArr.push('<div>高频词: <b>' + baseInfo.frequence + '/5</b>');
                        wordtxtatt.push('高频词: <b>' + baseInfo.frequence + '/5</b>');
                    }
                    //4
                    var wordTag = baseInfo.word_tag;
                    if (wordTag && wordTag.length > 0) {
                        var tagStr = ' ';
                        var tagDic = { '0': '考研', '1': 'CET6', '2': 'CET4', '3': 'GRE', '4': 'TOEFL', '5': 'IELTS' };
                        $(wordTag).each(function (i, ele) {
                            tagStr += tagDic[ele] + ' ';
                        })
                        wordStrArr.push(tagStr);
                        wordtxtatt.push(tagStr);
                    }
                    wordStrArr.push('</div>');
                    //追加单词解释
                    $('#wordmeans').html(wordStrArr.join(''));
                    //追加到生词本备用
                    window.document.wordtxt = wordtxtatt.join(' | ');

                } else {
                    $('#wordmeans').html('没有找到单词：' + word + '，请检查拼写是否正确。');
                }
            }
            else {
                // $('#wordmeans').html(data.errmsg)
                $('#wordmeans').html('没有找到单词：' + word + '，请检查拼写是否正确。');
            }
        }
    });
}
//追加单词联想（词组短语等）
function BuildSuggst(word) {
    var wordStrArr = [];
    $.ajax({

        url: 'http://dict-mobile.iciba.com/interface/index.php?c=word&m=getsuggest&nums=50&client=6&is_need_mean=1&word=' + word,
        type: 'get',
        dataType: 'jsonp',
        data: {
        },
        success: function (data) {
            if (data && data.status == 1) {
                var wordList = data.message;
                if (wordList && wordList.length > 0) {
                    for (var i = 0; i < wordList.length; i++) {
                        var word = wordList[i];
                        if (word && word.means && word.means.length > 0 && word.means[0].means) {
                            //var wordgroup = word.means[0].means.toString();
                            //if(wordgroup&&wordgroup.trim()&&wordgroup.indexOf(' ')>0)
                            if (word.key && word.key.trim() && word.key.indexOf(' ') > 0)
                                wordStrArr.push('<div><b>' + word.key + '</b> ' + word.means[0].means.toString() + '</div>');
                        }
                    }
                    if (wordStrArr.length > 0) {
                        $('#wordsuggst').html(wordStrArr.join(''));
                    }
                    else {
                        $('#wordsuggst').html('没有找到相关词组或短语。');
                    }
                } else {
                    $('#wordsuggst').html('没有找到相关词组或短语。');
                }
            } else {
                $('#wordsuggst').html('没有找到相关词组或短语。');
            }
        }
    });
}
//历史查询记录
function BuildHistory(word) {
    // debugger;
    { //$.data()用法
        //if ($('#searchhistory').data('d') == undefined)
        //    var words = [];
        //else
        //    var words = $('#searchhistory').data('d');
        //words.push(word);
        //$('#searchhistory').data('d', words);
    }
    //$.cookie('the_cookie', 'the_value', { expires: 7 }); //添加
    //$.cookie('d', null); //删除
    if ($.cookie('d') == undefined)
        var words = [];
    else
        var words = $.cookie('d').split(',');
    if (word) {
        var wordindex = jQuery.inArray(word, words);
        if (wordindex >= 0) {
            words.splice(wordindex, 1);
            words.unshift(word);
        }
        else {
            words.unshift(word);
            words.splice(5, 1);
        }
    }
    $.cookie('d', words.join(), { expires: 7 });
    var hishtml = [];
    $(words).each(function (i, ele) {
        hishtml.push('<span><a href="javascript:Search(\'' + ele + '\')" title="点击查询">' + ele + '</a></span>')
    });
    $('#searchhistory').html(hishtml.join(', '));

}
//加入生词本
function AddNewWord() {
    //debugger;
    if (!$.cookie('nd') || $.cookie('nd') == 'null')
        var words = [];
    else
        var words = $.cookie('nd').split('||');
    var wordtxt = window.document.wordtxt;
    if (wordtxt) {
        var wordindex = jQuery.inArray(wordtxt, words);
        if (wordindex >= 0) {
            //已存在
            $('#addmsg').text('已存在');
            //words.splice(wordindex, 1);
            //words.unshift(word);
        }
        else {
            //添加成功
            $('#addmsg').text('添加成功');
            //words.push(wordtxt);
            words.unshift(wordtxt);
            words.splice(10, 1);
        }
        $('#addmsg').fadeIn();
        setTimeout(function () {
            $('#addmsg').fadeOut(2000);

        }, 2000);
        //$('#addmsg').text(' ');
    }
    $.cookie('nd', words.join('||'), { expires: 7 });
    var newwordhtml = [];
    $(words).each(function (i, ele) {
        newwordhtml.push('<li class="list-group-item">' + (words.length - i) + '. <span>' + ele + '</span></li>')
    });
    $('#newwords').html(newwordhtml.join(''));

}
//清空英文文章
function ClearEnDoc() {
    $('#source').val('');

}
//清空生词本
function ClearNewWord() {
    if (confirm("确定清空生词本吗？")) {
        $('#newwords').html('');
        $.cookie('nd', null); //删除
    }
    else {
        return;
    }

}
//显示全文翻译
function BuildFullTrans() {
    alert('功能正在开发中...');
    return false;
    //
    var fulltext = $('#source').val();
    if (fulltext)

        $.ajax({

            'url': 'http://fanyi.baidu.com/v2transapi',//'http://fy.iciba.com/ajax.php?a=fy',
            'async': false,
            'type': 'POST',
            'dataType': 'jsonp',
            'data': {
                from: 'en',
                to: 'zh',
                query: fulltext,
                transtype: 'translang',
                simple_means_flag: 3
            },
            //{
            //    f: 'auto',
            //    t:'auto',
            //    w: fulltext
            //},
            'success': function (data) {
                //debugger;
                if (data && data.status == 1) {
                    var content = data.content;
                    if (content && content.ciba_out) {
                        $('#fulltrans').html(content.ciba_out);
                    } else {
                        $('#fulltrans').html(wordStrArr.join('没有找到相关内容。'));
                    }
                }
            }
            , 'error': function (data) {

                console.log(data);
            }, 'complete': function (data) {

                console.log(data);
            }
        });
}
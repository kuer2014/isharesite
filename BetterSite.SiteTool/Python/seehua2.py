'''安装requests
 pip install requests
Installing collected packages: chardet, certifi, idna, urllib3, requests
Successfully installed certifi-2017.4.17 chardet-3.0.4 idna-2.5 requests-2.18.2
urllib3-1.22
'''
#安装bs4
# pip install BeautifulSoup4
#Installing collected packages: BeautifulSoup4
#Successfully installed BeautifulSoup4-4.6.0
import bs4,requests
import json
import sys
def GetContent(url):
    global resultList
    res=requests.get(url)
    res.raise_for_status()
    noStarchSoup=bs4.BeautifulSoup(res.text,'html.parser')
    title=noStarchSoup.select('.entry-title')

    print(title[0].getText())
    #wfile.write(title[0].getText())
    #wfile.write('\n')
    content_p = noStarchSoup.select('.td-post-content p')
    all_p=""
    for _p in content_p:
        print(_p)
        #wfile.write(str(_p))
        #wfile.write('\n')
        all_p+=str(_p)
    #wfile.write('<p><small>(内容来源于网络)</small></p>')
    all_p +='<p><small>(内容来源于网络)</small></p>'
    resultDic = {"Title": title[0].getText(),"Content":all_p}

    resultList.append(resultDic)
    print('resultList:' + str(resultList))
    #wfile.write('\r\n')
    nextUrl=noStarchSoup.select('.td-post-next-prev-content a')[0].attrs['href']
    print('nextUrl:'+nextUrl)
    global _i
    _i -= 1
    if(_i>0 and nextUrl!=None):
       return GetContent(nextUrl)
    else:
        return resultList
if(len(sys.argv)==3):
    # print('请输入URL地址：')
    # url=input() #http://news.seehua.com/?p=292391
    # print('请输入条数：')
    # _i=int(input())   #采集文章数
    url=sys.argv[1]
    _i=int(sys.argv[2])
    wfile = open(r'sitestxt.txt', 'w',encoding='utf-8')
    resultList=[]
    GetContent(url)   #娱乐分类
    wfile.write(json.dumps(resultList,ensure_ascii=False))
    wfile.close()
else:
    print('-1') #参数有误

# SQLAlchemy ORM
#python db-api http://tech.it168.com/a2009/1014/759/000000759444.shtml
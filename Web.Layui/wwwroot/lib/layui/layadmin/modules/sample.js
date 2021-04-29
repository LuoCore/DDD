/** layuiAdmin.std-v1.2.1 LPPL License By http://www.layui.com/admin/ */
;
layui.define(function (e) {
    var $ = layui.jquery,
    a = layui.admin,
    request_data = {
        load_all_send: function () {
            $.post("/Inside/LoadAllSend", {}, function (res) {
                console.log(res);
                return res;
            });
        }
    };
    layui.use(["admin", "carousel"],
    function () {
        var e = layui.$,
        a = (layui.admin, layui.carousel),
        l = layui.element,
        t = layui.device();
        e(".layadmin-carousel").each(function () {
            var l = e(this);
            a.render({
                elem: this,
                width: "100%",
                arrow: "none",
                interval: l.data("interval"),
                autoplay: l.data("autoplay") === !0,
                trigger: t.ios || t.android ? "click" : "hover",
                anim: l.data("anim")
            })
        }),
        l.render("progress")
    }),
   
    layui.use(["carousel", "echarts"],      
    function () {
        var e = layui.$,
        a = (layui.carousel, layui.echarts),
        l = [],
        t = [{
            tooltip: {
                trigger: "axis"
            },
            calculable: !0,
            legend: {
                data: ["发送量", "失败量"]
            },
            xAxis: [{
                type: "category",
                data: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月"]
            }],
            yAxis: [{
                type: "value",
                name: "发送量",
                axisLabel: {
                    formatter: "{value} 条"
                }
            }],
            series: [{
                name: "发送量",
                type: "line",
                data: []
            },
            ]
        }],
        m = [{
            series: [{
                name: "发送量",
                type: "line",
                data: request_data.load_all_send(),
            }]
        }],
        i = e("#lay-month-statistics").children("div"),
        n = function (e) {
            l[e] = a.init(i[e], layui.echartsTheme),
            l[e].setOption(t[e]),
            l[e].setOption(m[e]),
            window.onresize = l[e].resize
        };
        i[0] && n(0)
    }),
  
    layui.use("table",
    function () {
        var e = (layui.$, layui.table);
        e.render({
            elem: "#LAY-index-prograss",
            url: layui.setter.base + "json/console/prograss.js",
            cols: [[{
                type: "checkbox",
                fixed: "left"
            },
            {
                field: "prograss",
                title: "任务"
            },
            {
                field: "time",
                title: "所需时间"
            },
            {
                field: "complete",
                title: "完成情况",
                templet: function (e) {
                    return "已完成" == e.complete ? '<del style="color: #5FB878;">' + e.complete + "</del>" : "进行中" == e.complete ? '<span style="color: #FFB800;">' + e.complete + "</span>" : '<span style="color: #FF5722;">' + e.complete + "</span>"
                }
            }]],
            skin: "line"
        })
    }),
    a.events.replyNote = function (e) {
        var a = e.data("id");
        layer.prompt({
            title: "回复留言 ID:" + a,
            formType: 2
        },
        function (e, a) {
            layer.msg("得到：" + e),
            layer.close(a)
        })
    },
    e("sample", {})
});
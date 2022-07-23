
import http from "../http.js";


export function TipsFilter(str) { 
    str = str.replace(/([\d]+[\s]*、[^\d])/g, "<br>$1");     // 2、开始
    str = str.replace(/(；)([\d]+)/g, "$1<br>$2");           // ；2开始
    str = str.replace(/(（[\d]+）)/g, "<br>$1");             // （2）
    str = str.replace(/[<br>]+/g,"\n");
    return str;
}



// #region 课程列表与分类
export const TAGS1 = [
    new Tag("1", "前庭平衡"),
    new Tag("2", "重力安适"),
    new Tag("3", "触觉调和"),
    new Tag("4", "协调运动"),
    new Tag("5", "综合训练"),
],
    TAGS2 = [
        new Tag("O", "O课堂说明"),
		new Tag("A", "A开场游戏"),
		new Tag("B", "B无教具&被动类"),
		new Tag("C", "C钻 & 滚"),
		new Tag("D", "D爬行类"),
		new Tag("E", "E跳跃类"),
		new Tag("F", "F走 & 跑"),
		new Tag("G", "G大龙球类"),
		new Tag("H", "H花生球 & 羊角球"),
		new Tag("J", "J平衡台项目"),
		new Tag("K", "K独脚凳项目"),
		new Tag("L", "L大陀螺项目"),
		new Tag("M", "M大A字架项目"),
		new Tag("P", "P滑板车项目"),
		new Tag("Q", "Q蹦床项目"),
		new Tag("R", "R彩虹桥"),
		new Tag("S", "S¼圈项目"),
		new Tag("T", "T万象组合项目"),
		new Tag("U", "U小球类"),
		new Tag("V", "V小教具 & 组合操作"),
		new Tag("W", "W攀爬架/综合游戏设计"),
		new Tag("X", "XY其他"),
		new Tag("Z", "Z我的自定义项目"),
    ];
export function getCoursesAll() { 
    return new Promise((resolve, reject) => {
        http.get("./api/gailan/getCourses.aspx")
            .then(ret => {
                if (!ret.code) {
                    reject("发生错误！");
                    return;
                }
                ret.list.forEach(element => {
                    element.tip = TipsFilter(element.tip);
                    element.tips = element.tip.split("\n").filter(s => s);
                    
                    element.tags = [getTag1(element.mId), getTag2(element.enName, element.uploadUser)];
                });
                ret.list.sort((a, b) => {
                    return a.enName > b.enName ? 1 : -1;
                });

                resolve(ret.list);
            })
            .catch(error => {
                reject(error);
            });
    });
}


function getTag1(mId){
	mId = mId + "";
	var tag = null;
	TAGS1.forEach(ta => {
		if(ta.id === mId){
			tag = ta;
		}
	});
	if(!tag) tag = TAGS1[0];
	return tag;
}
function getTag2(enName,uploadUser){
	var tag = null;
	TAGS2.forEach(ta => {
		if(enName.match(new RegExp("^" + ta.id))){
			tag = ta;
		}
	});
	if(uploadUser!="") tag = TAGS2[TAGS2.length - 1];
	if(!tag) tag = TAGS2[TAGS2.length - 2];
	return tag;
}

/**
 * Tag标签对象构造类
 * @param id 标签id
 * @param cnName 标签中文名
*/
function Tag(id, cnName) {
    this.id = id;
    this.cnName = cnName;
}
// #endregion
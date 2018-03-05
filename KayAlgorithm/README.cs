/********************************************************************************
** All rights reserved
** Auth： kay.yang
** E-mail: 1025115216@qq.com
** Date： 7/8/2017 9:49:55 AM
** Version:  v1.0.0
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KayAlgorithm
{
    /*
 
     * 已实现算法
     * 1 BVHTree2 
     * 2 多边形三角化   (EarCliping)
     * 3 多边形凸分解   (基于EarClipping和H-M算法， 没处理Rogers算法)
     * 4 动态生成路点   (基于凸分解算法，此算法还需要进一步处理)
     * 5 骨骼提取       (图像处理算法， 查表法)
     * 6 2d凸包        (Jarvis Step算法)
     * 7 多边形填充     (使用传统扫描线算法)
     * 8 凸多边形填充   (此算法针对凸多边形，性能更快)  
     * 9 矩形分区       (二叉树和贪心算法)
     * 10 凸多边形分区 查询凸多边形最大矩形 查询凸多边形top k大矩形 (贪心)
     * 11 3d 凸包       (QuickHull算法)
     * 12 BVHTree3      (射线跟踪，快速搜索)
     * 13 SM2国密加密算法  (非对称，比RSA快，安全)
     * 14 AStarFast     (A*寻路算法)
     * 15 基于元素相近性聚类  （GroupClosest)
     * 16 KMeans+聚类    (对比传统聚类，增加了 初始值 优化处理)
     * 17 FPGrowth关联算法
     * 18 基于Hash分组  (GroupHash, 特定情况下可使用，性能比GroupClosest快)
     * 
     * 
    
     * 接下来有时间需实现的算法
     * 1 Dijkstra        
     * 2 BFS DFS
     * 3 Steer
     * 4 BipartiteMatch  (二部图匹配)
     * 5 EJK
     * 6 EPA
     * 7 矩阵求逆
     * 
     * 
 
     * 辅助类
     * 1 DrawLineUtils 保存到BMP图片文件
     * 2 KeyedPriorityQueue 优先队列 (若需要响应 Head 改变事件时， 元素需重载 Equal 函数)
     * 3 合并顶点
     * 4 查找边界
     * 
     */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachDestroy : MonoBehaviour
{
	void Update ()
	{
	   
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "SadPlane")
        {
            RaycastHit hit;

            // 两个物体接触的第一个点作为射线位置
            Ray ray = Camera.main.ScreenPointToRay(col.contacts[0].point);

            

           

            if (Physics.Raycast(ray, out hit))
            {
                //deleteTri(hit.triangleIndex, col);
                Destroy(col.gameObject);
            }
           
        }
    }

    void deleteTri(int index, Collision col)
    {
        // 移除对象的碰撞体
        //Destroy(this.gameObject.GetComponent<MeshCollider>());
        Destroy(col.gameObject.GetComponent<MeshCollider>());
        //// 获取对象的全部网格
        Mesh mesh = transform.GetComponent<MeshFilter>().mesh;

        //// 把获取的网格中的三角形顶点放在一个数组里
        int[] oldTriangles = mesh.triangles;
        //// 新三角形的定点总和减去三，因为点击了一次一个三角形被移除了，也就时三个定点被移除了
        int[] newTriangles = new int[mesh.triangles.Length - 3];

        int i = 0;
        int j = 0;
        //// 遍历所有三角形的顶点
        while (j < mesh.triangles.Length)
        {
            if (j != index * 3)
            {
                // 每次都向前合成三个顶点
                newTriangles[i++] = oldTriangles[j++];
                newTriangles[i++] = oldTriangles[j++];
                newTriangles[i++] = oldTriangles[j++];
            }
            else
            {
                // 利用三角形序列数与三的倍数关系得到空缺三角形的三个定点的第一个顶点的号码
                j += 3;
            }
        }
        //// 筛选结束，已经剔除了应该删除的三角形
        //// 新建一个网格
        transform.GetComponent<MeshFilter>().mesh.triangles = newTriangles;
        //// 为网格加上碰撞体
        this.gameObject.AddComponent<MeshCollider>();
    }
}

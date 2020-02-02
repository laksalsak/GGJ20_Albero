using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BezierSolution;

namespace UnifiedRoadRailSystem.Core
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class RailGraphics : MonoBehaviour
    {

        public float tilingX, tilingY;
        public float wireHeight = 5.2f;
        public float wireSize = 0.04f;

        public Material wireMaterial;

        private void Update()
        {
            if (TreeController.instance.spline==null) return;
            if (this.transform.parent.gameObject != TreeController.instance.selectedSezione) return;
            if (TreeController.instance.spline.GetPoints().Length > 2)
            {
                MakeWireMesh(TreeController.instance.spline.GetPoints(), this.transform.lossyScale.x);
            }
         
        }
       
        public void MakeWireMesh(Vector3[] pointsA, float scale)
        {
           // this.transform.position = Vector3.zero;
            //  verso = _verso;
            Mesh mesh;

            mesh = CreateCavo(pointsA,scale);


            MeshFilter mf = this.GetComponent<MeshFilter>();
            if (mf == null)
            {
                mf = this.gameObject.AddComponent<MeshFilter>();
            }
            MeshRenderer mr = this.GetComponent<MeshRenderer>();
            if (mr == null)
            {
                mr = this.gameObject.AddComponent<MeshRenderer>();
            }


            mf.mesh = mesh;

            int textureRepeat = Mathf.RoundToInt(tilingX);
            if (mr.sharedMaterial != null)
                mr.sharedMaterial.mainTextureScale = new Vector2(tilingY, textureRepeat);

            if (mf.sharedMesh != null)
                mf.sharedMesh.RecalculateNormals();
            //GetComponent<MeshCollider>().sharedMesh = mesh[2];
        }



       

        Mesh CreateCavo(Vector3[] points,float scale)
        {
            Vector3[] verts = new Vector3[points.Length * 3];
            Vector2[] uvs = new Vector2[verts.Length];
            int numTris = 6 * (points.Length - 1);

            if (numTris < 0) return null;

            int[] tris = new int[numTris * 3];
            int vertIndex = 0;
            int triIndex = 0;
            Mesh mesh = new Mesh();
            float uv = 0;
            //Vector3 up = new Vector3(0, 1, 0);
          
            for (int i = 0; i < points.Length; i++)
            {

                Vector3 forward;
                if (i < points.Length - 1)
                {
                    forward = (points[i + 1] - points[i]).normalized;
                }
                else
                {
                    forward = ((points[points.Length - 1] - points[points.Length - 2]).normalized);
                }
                Vector3 offset = -transform.position+  (points[i]);
                Vector3 left = (Quaternion.Euler(90, 0, 0) * forward).normalized;//new Vector3(forward.x, forward.y, 0);
                Vector3 up  = (Quaternion.Euler(0, 0, 90) * forward).normalized;//new Vector3(forward.x, forward.y, 0);
                Debug.DrawRay(points[i], up, Color.red, 10);

                verts[vertIndex] =  (offset) + up * (wireHeight) ; 
                verts[vertIndex + 1] = (offset) + up * (wireHeight * 1.73f * wireSize) - left * wireSize * 0.5f;
                verts[vertIndex + 2] = (offset) - up * (wireHeight *1.73f * wireSize) + left * wireSize * 0.5f;

                if (uv == 0) //pari
                {
                    uv = 1;
                }
                else
                {
                    uv = 0;
                }
                float v = uv;

                uvs[vertIndex] = new Vector2(0f, v);
                uvs[vertIndex + 1] = new Vector2(0.5f, v);
                uvs[vertIndex + 2] = new Vector2(1f, v);

                int vl = verts.Length;

                if (i < points.Length - 1)
                {
                    MakeTris(ref tris, ref triIndex, 0, 1, 4, vertIndex, vl, 1);
                    MakeTris(ref tris, ref triIndex, 4, 3, 0, vertIndex, vl, 1);
                    MakeTris(ref tris, ref triIndex, 4, 1, 5, vertIndex, vl, 1);
                    MakeTris(ref tris, ref triIndex, 5, 1, 2, vertIndex, vl, 1);
                    MakeTris(ref tris, ref triIndex, 2, 0, 5, vertIndex, vl, 1);
                    MakeTris(ref tris, ref triIndex, 3, 5, 0, vertIndex, vl, 1);
                }

                vertIndex += 3;
                //triIndex += 18;
            }
            mesh.vertices = verts;
            mesh.triangles = tris;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
            mesh.RecalculateTangents();
            return mesh;
        }

        public static void MakeTris(ref int[] tris, ref int index, int a, int b, int c, int vertIndex, int vertLenght, int verso)
        {
            if (verso >= 0)
            {
                tris[index + 0] = (vertIndex + a) % vertLenght;
                tris[index + 1] = (vertIndex + b) % vertLenght;
            }
            else
            {
                tris[index + 0] = (vertIndex + b) % vertLenght;
                tris[index + 1] = (vertIndex + a) % vertLenght;
            }
            tris[index + 2] = (vertIndex + c) % vertLenght;
            index += 3;
        }

        private Mesh CombineMeshes(List<Mesh> meshes)
        {
            if (meshes.Count == 1) return meshes[0];

            var combine = new CombineInstance[meshes.Count];
            for (int i = 0; i < meshes.Count; i++)
            {
                combine[i].mesh = meshes[i];
                combine[i].transform = transform.localToWorldMatrix;
            }

            var mesh = new Mesh();
            mesh.CombineMeshes(combine);
            return mesh;
        }
    }
}


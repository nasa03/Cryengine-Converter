﻿using CgfConverter;
using CgfConverter.CryEngineCore;
using CgfConverterTests.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace CgfConverterTests.IntegrationTests
{
    [TestClass]
    public class CgfConverterIntegrationTests
    {
        private readonly TestUtils testUtils = new TestUtils();
        string userHome;

        [TestInitialize]
        public void Initialize()
        {
            testUtils.errors = new List<string>();
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
            userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            testUtils.GetSchemaSet();
        }


        [TestMethod]
        public void SimpleCubeSchemaValidation()
        {
            testUtils.ValidateXml($@"{userHome}\OneDrive\ResourceFiles\simple_cube.dae");
            Assert.AreEqual(0, testUtils.errors.Count);
        }

        [TestMethod]
        public void SimpleCubeSchemaValidation_BadColladaWithOneError()
        {
            testUtils.ValidateXml($@"{userHome}\OneDrive\ResourceFiles\simple_cube_bad.dae");
            Assert.AreEqual(1, testUtils.errors.Count);
        }


        [TestMethod]
        public void brfl_rifle_NoMtlFilev802_CreatesDummyInstanceMaterial()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\brfl_fps_behr_p4ar_body.cgf", "-dds", "-dae" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(17, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        [TestMethod]
        public void UnknownSource_forest_ruin()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\forest_ruin.cgf", "-dds", "-dae", "-objectdir", @"..\..\ResourceFiles\" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(13, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        [TestMethod]
        public void GhostSniper3_raquel_eyeoverlay_skin()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\Test01\raquel_eyeoverlay.skin", "-dds", "-dae", "-objectdir", @"..\..\ResourceFiles\Test01\" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(6, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        [TestMethod]
        public void Prey_Dahl_GenMaleBody01_MaterialFileFound()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\Prey\Dahl_GenMaleBody01.skin", "-dds", "-dae", "-objectdir", @"..\..\ResourceFiles\Prey\" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(1, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        [TestMethod]
        public void Prey_Dahl_GenMaleBody01_MaterialFileNotAvailable()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\Prey\Dahl_GenMaleBody01.skin" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(1, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        [TestMethod]
        public void Evolve_griffin_skin_NoMaterialFile()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\Evolve\griffin.skin" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(15, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        [TestMethod]
        public void Evolve_griffin_menu_harpoon_skin_NoMaterialFile()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\Evolve\griffin_menu_harpoon.skin" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(2, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        [TestMethod]
        public void Evolve_griffin_fp_skeleton_chr_NoMaterialFile()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\Evolve\griffin_fp_skeleton.chr" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(3, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        [TestMethod]
        public void UnknownSource_osv_96_muzzle_brake_01_fp_NoMaterialFile()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\osv_96_muzzle_brake_01_fp.cgf" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(5, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        [TestMethod]
        public void UnknownSource_spriggan_proto_mesh_skin_NoMaterialFile()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\spriggan_proto_mesh.skin" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Assert.AreEqual((uint)41, cryData.Models[0].NumChunks);
            Assert.AreEqual(ChunkType.Node, cryData.Models[0].ChunkMap[47].ChunkType);
            var datastream = cryData.Models[0].ChunkMap[40] as ChunkDataStream;
            Assert.AreEqual((uint)12, datastream.BytesPerElement);
            Assert.AreEqual((uint)22252, datastream.NumElements);
            Assert.AreEqual(0.29570183, datastream.Vertices[0].X, TestUtils.delta);
            Assert.AreEqual(0.42320457, datastream.Vertices[0].Y, TestUtils.delta);
            Assert.AreEqual(3.24175549, datastream.Vertices[0].Z, TestUtils.delta);

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(2, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        [TestMethod]
        public void UnknownSource_spriggan_proto_skel_chr_NoMaterialFile()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\spriggan_proto_skel.chr" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(1, actualMaterialsCount);

            testUtils.ValidateColladaXml(colladaData);
        }

        // Model appears to be broken.  Assigns 3 materials, but only 2 materials in mtlname chunks
        [TestMethod]
        public void Cnylgt_marauder_NoMaterialFile()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\cnylgt_marauder.cga" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new CryEngine(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new Collada(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(2, actualMaterialsCount);
        }

        [TestMethod]
        public void Green_fern_bush_a_MaterialFileExists()
        {
            var args = new string[] { $@"{userHome}\OneDrive\ResourceFiles\CryEngine\green_fern_bush_a.cgf" };
            int result = testUtils.argsHandler.ProcessArgs(args);
            Assert.AreEqual(0, result);
            CryEngine cryData = new(args[0], testUtils.argsHandler.DataDir.FullName);
            cryData.ProcessCryengineFiles();

            Collada colladaData = new(testUtils.argsHandler, cryData);
            colladaData.GenerateDaeObject();

            int actualMaterialsCount = colladaData.DaeObject.Library_Materials.Material.Count();
            Assert.AreEqual(3, actualMaterialsCount);
            var libraryGeometry = colladaData.DaeObject.Library_Geometries;
            Assert.AreEqual(3, libraryGeometry.Geometry.Length);

            // Validate geometry and colors
            var mesh = colladaData.DaeObject.Library_Geometries.Geometry[0].Mesh;
            Assert.AreEqual(4, mesh.Source.Length);
            Assert.AreEqual(2, mesh.Triangles.Length);
            var vertices = mesh.Source[0];
            var normals = mesh.Source[1];
            var uvs = mesh.Source[2];
            var colors = mesh.Source[3];
            Assert.IsTrue(vertices.Float_Array.Value_As_String.StartsWith("0.234264 0.802049 0.952682 0.171377 0.854397 0.869569 0.118639 0.752597 0.880529 0.302113"));
            Assert.IsTrue(normals.Float_Array.Value_As_String.StartsWith("-0.486369 0.300190 0.820567 -0.605179 0.386607 0.695912 -0.628418 0.290691 0.721519 -0.521254"));
            Assert.IsTrue(uvs.Float_Array.Value_As_String.StartsWith("0.520376 0.339241 0.506553 0.148210 0.582104 0.149441 0.421715 0.334883 0.408283"));
            Assert.IsTrue(colors.Float_Array.Value_As_String.StartsWith("0.388235 0.854902 0.635294 0.831373 1 0.854902 0.615686 0.827451 1 0.854902 0.690196"));
            
            // Validate Triangles
            Assert.AreEqual(918, mesh.Triangles[0].Count);
            Assert.AreEqual("green_fern_bush-material", mesh.Triangles[0].Material);
            Assert.IsTrue(mesh.Triangles[0].P.Value_As_String.StartsWith("0 0 0 0 1 1 1 1 2 2 2 2 3 3 3 3 4 4 4 4 1 1 1 1 1 1 1 1 0 0 0 0 3 3 3 3 5"));
            Assert.AreEqual(4, mesh.Triangles[0].Input.Length);

            // Validate image library
            var images = colladaData.DaeObject.Library_Images;
            Assert.AreEqual(3, images.Image.Length);
            Assert.AreEqual("green_fern_bush_Diffuse", images.Image[0].ID);
            Assert.AreEqual("textures/green_fern_bush_leaf_a.dds", images.Image[0].Init_From.Uri);

            // Validate visual_scene
            var visualScene = colladaData.DaeObject.Library_Visual_Scene;
            Assert.AreEqual(1, visualScene.Visual_Scene.Length);
            Assert.AreEqual("Scene", visualScene.Visual_Scene[0].ID);
            var nodes = visualScene.Visual_Scene[0].Node;
            Assert.AreEqual(1, nodes.Length);
            Assert.AreEqual(6, nodes[0].node.Length);
            Assert.AreEqual("green_fern_bush_a", nodes[0].ID);
            Assert.AreEqual("green_fern_bush_a", nodes[0].ID);
            Assert.AreEqual("NODE", nodes[0].Type.ToString());
            Assert.AreEqual("0.993981 0.109553 -0 0 -0.109553 0.993981 -0 0 -0 0 1 0 0 0 0 1", nodes[0].Matrix[0].Value_As_String);
            Assert.AreEqual("green_fern_bush_a", nodes[0].Instance_Geometry[0].Name);
            Assert.AreEqual("#green_fern_bush_a-mesh", nodes[0].Instance_Geometry[0].URL);
            Assert.AreEqual("#green_fern_bush-material", nodes[0].Instance_Geometry[0].Bind_Material[0].Technique_Common.Instance_Material[0].Target);
            Assert.AreEqual("green_fern_bush-material", nodes[0].Instance_Geometry[0].Bind_Material[0].Technique_Common.Instance_Material[0].Symbol);
            Assert.AreEqual("#proxy_AI-material", nodes[0].Instance_Geometry[0].Bind_Material[0].Technique_Common.Instance_Material[1].Target);
            Assert.AreEqual("proxy_AI-material", nodes[0].Instance_Geometry[0].Bind_Material[0].Technique_Common.Instance_Material[1].Symbol);

            testUtils.ValidateColladaXml(colladaData);
        }
    }
}


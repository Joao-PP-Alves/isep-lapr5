import { TrackballControls } from "three";
import Links from "../components/Links";
import * as THREE from "three";
import Edge from "./edge";
import Node from "./node";
import camera_zoom from "./camera_zoom";
import Orientation from "./orientation";
import { camera_const } from "./camera_const";
import { OrbitControls } from "three/examples/jsm/controls/OrbitControls";
//import {returnRows} from './getMyFriends';

let rows = [];
function createData(id, name, parent, forcaLigacao, forcaRelacao) {
	return { id, name, parent, forcaLigacao, forcaRelacao };
}

const userId = localStorage.getItem("loggedInUser");

let colors = [];

const populateRows = async () => {
	colors.push(0xff0000);
	colors.push(0x00ff00);
	colors.push(0x0000ff);
	colors.push(0x888888);
	colors.push(0xff00ff);
	colors.push(0x00dddd);
	colors.push(0xcccc00);
	colors.push(0x0f0f0f);
	colors.push(0x0fff0f);
	colors.push(0xfff0dd);
	// DATA GET FROM MASTER DATA API
	await fetchUsers();
    sampleToRows();

	//BEFORE DATA BASE
	/*rows.push(createData("1", "Ferndando", null, "3", "10"));
	rows.push(createData("2", "Ferndanda", "1", "4", "10"));
	rows.push(createData("3", "Ricardo", "1", "5", "10"));
	rows.push(createData("4", "Luísa", "1", "6", "10"));
	rows.push(createData("5", "Lurdes", "1", "7", "10"));
	rows.push(createData("6", "Raquel", "2", "8", "10"));
	rows.push(createData("7", "Olavo", "2", "9", "10"));
	rows.push(createData("8", "Rajesh", "6", "10", "10"));
	rows.push(createData("9", "Rita", "3", "11", "10"));
	rows.push(createData("10", "Rute", "4", "12", "10"));
	rows.push(createData("11", "Diogo", "4", "13", "10"));*/
};

let searchedVS = [];
const fetchUsers = async () => {
	const data = await fetch(
		Links.MDR_URL() + "users/MyPerspective/" + userId + "/" + 1
	);
	const vsList = await data.json();
	searchedVS = vsList;

	fillSample();
};

let sample = [];

function fillSample() {
	for (var i = 0; i < searchedVS.length; i++) {
		var obj = searchedVS[i];

		try {
			sample.push([
				obj.userId,
				obj.userName,
				obj.parentId,
				obj.connectionStrength,
				obj.relationshipStrength,
			]);
		} catch {
			sample.push([
				obj.userId,
				obj.userName,
				obj.parentId,
				obj.connectionStrength,
				obj.relationshipStrength,
			]);
		}
	}
}

// push the information from sample to rows

rows = [];
const sampleToRows = () => {
	for (let i = 0; i < searchedVS.length; i += 1) {
		rows.push(createData(...sample[i]));
	}
};

const returnRows = async () => {
	await populateRows();
	return rows;
};

export default class Graph {
	nodes = [];
	edges = [];
	constructor(canvasRef) {
		this.canvas = canvasRef;
		this.renderer = new THREE.WebGLRenderer({
			canvas: canvasRef,
			antialias: true,
			alpha: true,
		});

		this.renderer.setPixelRatio(window.devicePixelRatio);

		this.renderer.setSize(window.innerWidth * 0.99, window.innerHeight * 0.99);

		document.body.appendChild(this.renderer.domElement);

		this.scene = new THREE.Scene();

		var aspectRatio = window.innerWidth / window.innerHeight;
		this.camera = new THREE.PerspectiveCamera(75, aspectRatio, 0.1, 100);
		const controls = new OrbitControls(this.camera, this.renderer.domElement);
		controls.enableRotate = false;
		controls.maxDistance = 100;
		controls.minDistance = 5;

		this.camera.position.set(0, 0, 100);
		controls.update();

		this.scene.add(this.camera);

		this.cameraMinimap = new THREE.PerspectiveCamera(
			90,
			window.innerWidth / window.innerHeight,
			0.1,
			100
		);
		this.cameraMinimap.position.set(0, 0, 100);

		this.scene.add(this.cameraMinimap);

		this.light = new THREE.AmbientLight(0x404040);
		this.light.position.z = 100;
		this.scene.add(this.light);

		var dtos = returnRows().then((response) => {
			this.createNodes(response);
			var lvl = 0;
			this.addNodesToScene(this.rootNode, lvl);

			this.addEdgesToScene(this.nodes, response);
		});

		this.miniMapCameraParameters = merge(true, camera_const, {
			view: "mini-map",
			multipleViewsViewport: new THREE.Vector4(1, 0.04, 0.4, 0.2),
			initialOrientation: new Orientation(180.0, 0.0),
			initialZoom: 0.7,
		});
		this.miniMapCamera = new camera_zoom(
			this.miniMapCameraParameters,
			window.innerWidth,
			window.innerHeight
		);

		this.topViewCameraParameters = merge(true, camera_const, {
			view: "top",
			initialOrientation: new Orientation(0.0, -90.0),
			initialZoom: 0.7,
		});
		this.topViewCamera = new camera_zoom(
			this.topViewCameraParameters,
			window.innerWidth,
			window.innerHeight
		);
		this.update();
	}

	getCanvas() {
		return this.canvas;
	}

	update() {
		this.renderer.render(this.scene, this.camera);
		this.render();

		requestAnimationFrame(this.update.bind(this));
	}

	addEdgesToScene(nodes, dtos) {
		nodes.forEach((node) => {
			var adjacentes = node.adjacents;

			adjacentes.forEach((adj) => {
				dtos.forEach((dto) => {
					if (dto.parent === adj.parent && dto.id === adj.user) {
						var edge = new Edge(
							{
								parent: node,
								friend: adj,
								ligacao: dto.forcaLigacao,
								relacao: dto.forcaRelacao,
							},
							this.scene
						);
						this.edges.push(edge);
					}
				});
			});
		});
	}

	createNodes(dtos) {
		dtos.forEach((dto) => {
			var node = new Node({
				color: 0x00ff00,
				radius: 3,
				user: dto.id,
				parent: dto.parent,
				adjacents: [],
				x: 0,
				y: 0,
				angle: 0,
				angleRange: 2 * Math.PI,
				depth: 0,
			});
			if (dto.parent === null) {
				this.rootNode = node;
			}
			this.nodes.push(node);
		});
		dtos.forEach((dto) => {
			this.nodes.forEach((node) => {
				if (dto.id === node.user) {
					var adjacentes = [];
					dtos.forEach((dto2) => {
						if (dto2.parent === dto.id) {
							this.nodes.forEach((node2) => {
								if (node2.user === dto2.id) {
									adjacentes.push(node2);
								}
							});
						}
					});
					node.addAdjacents(adjacentes);
				}
			});
		});
	}

	addNodesToScene(node, lvl) {
		if (node.parent === null) {
			node.setNewColor(colors[lvl]);
			node.initialize(this.scene);
		}

		var n = node.adjacents.length;
		lvl = lvl + 1;
		for (var i = 0; i < node.adjacents.length; i++) {
			var center = 0;
			if (node.adjacents[i].parent !== null) {
				node.adjacents[i].setNewColor(colors[lvl]);

				center = (-node.angleRange + node.angleRange / n) * 0.5;
				node.adjacents[i].depth = lvl;
				node.adjacents[i].angle =
					node.angle + (node.angleRange / n) * i + center;

				node.adjacents[i].angleRange = node.angleRange / n;

				var posX =
					25 * node.adjacents[i].depth * Math.cos(node.adjacents[i].angle) -
					0 * Math.sin(node.adjacents[i].angle);

				var posY =
					25 * node.adjacents[i].depth * Math.sin(node.adjacents[i].angle) +
					0 * Math.cos(node.adjacents[i].angle);

				node.adjacents[i].x = posX;

				node.adjacents[i].y = posY;

				node.adjacents[i].initialize(this.scene);

				this.addNodesToScene(node.adjacents[i], lvl);
			}
		}
	}

	render() {
		this.frameId = requestAnimationFrame(() => {
			this.render();
		});

		const viewportTop = this.topViewCamera.getViewport();
		this.renderer.setViewport(
			viewportTop.x,
			viewportTop.y,
			viewportTop.width,
			viewportTop.height
		);
		this.renderer.setScissor(
			viewportTop.x,
			viewportTop.y,
			viewportTop.width,
			viewportTop.height
		);
		this.renderer.setScissorTest(true);
		this.renderer.render(this.scene, this.camera);

		const viewport = this.miniMapCamera.getViewport();
		this.renderer.setViewport(
			viewport.x,
			viewport.y,
			viewport.width,
			viewport.height
		);
		this.renderer.setScissor(
			viewport.x,
			viewport.y,
			viewport.width,
			viewport.height
		);
		this.renderer.setScissorTest(true);
		this.renderer.render(this.scene, this.cameraMinimap);
	}

	resize() {
		const width = window.innerWidth;
		const height = window.innerHeight;

		this.camera.aspect = width / height;
		this.camera.updateProjectionMatrix();

		this.topViewCamera.updateWindowSize(window.innerWidth, window.innerHeight);
		this.miniMapCamera.updateWindowSize(window.innerWidth, window.innerHeight);

		this.renderer.setSize(width, height);
	}
}

function merge(deep, ...sources) {
	let target = {};
	for (let i = 0; i < sources.length; i++) {
		let source = sources[i];
		if (deep) {
			for (let key in source) {
				const value = source[key];
				if (
					value instanceof Object &&
					!(value instanceof THREE.Vector2) &&
					!(value instanceof THREE.Vector3) &&
					!(value instanceof THREE.Vector4)
				) {
					target[key] = merge(true, target[key], value);
				} else {
					target[key] = value;
				}
			}
		} else {
			target = Object.assign(target, source);
		}
	}
	return target;
}

import React, { useState, createRef, useEffect } from 'react';
import Links from "../components/Links";
import * as THREE from 'three';
import Edge from './edge';
import Node from './node';
import renderToCanvas from './renderToCanvas';
//import {returnRows} from './getMyFriends';


let rows = [createData("1","Ferndando",null,"3","1"),
createData("2","Ferndanda","1","3","1"),
createData("3","Ricardo","1","3","1"),
createData("4","Luísa","1","3","1"),
createData("5","Lurdes","1","3","1"),
createData("6","Raquel","2","3","1"),
createData("7","Olavo","2","3","1"),
createData("8","Rajesh","6","3","1"),
createData("9","Rita","3","3","1"),
createData("10","Rute","4","3","1"),
createData("11","Diogo","4","3","1")
];
function createData( id, name,parent ,forcaLigacao, forcaRelacao) {
    return { id, name,parent ,forcaLigacao, forcaRelacao};
}

/** function ListMyFriendsContent() {

    
    const userId = localStorage.getItem('loggedInUser');

    const [open, setOpen] = React.useState(true);
    const toggleDrawer = () => {
      setOpen(!open);
    };
  
    useEffect(() => {
      search();
    }, []);

    function search() {
        getData();
    }

    const [searchedVS, setSearchedVS] = useState([]);
    const getData= async () => {
        
        const data = await fetch(
            Links.MDR_URL() + "/api/users/getPrespective/" + userId + '/' + 3
        );
        const vsList = await data.json();
        console.log(vsList);
        setSearchedVS(vsList);
    };
    let sample = [];

for (var i = 0; i < searchedVS.length; i++){
  var obj = searchedVS[i];
  console.log(obj);
  
try{
  sample.push([obj.userId,obj.userName,obj.parentId,obj.connectionStrength,obj.relationshipStrength]);
}catch{
  sample.push([obj.userId,obj.userName,obj.parentId,obj.connectionStrength,obj.relationshipStrength]);
}
}

function createData( id, name,parent ,forcaLigacao, forcaRelacao) {
  return { id, name,parent ,forcaLigacao, forcaRelacao};
}


// push the information from sample to rows

rows = [];

for (let i = 0; i < searchedVS.length; i += 1) {
  rows.push(createData(...sample[i]));
}
}
*/
function returnRows(){
    //ListFriends();
    return rows;
}

/** function ListFriends() {
    return <ListMyFriendsContent />;
}
*/
export default class Graph {
    scene;
    camera;
    renderer;
    nodes = [];
    edges = [];
    rootNode;
    constructor(div){
        this.scene = new THREE.Scene();
        this.camera = new THREE.PerspectiveCamera(
          75,
          window.innerWidth / window.innerHeight,
          0.1,
          1000
        );
        this.renderer = new THREE.WebGLRenderer();
        this.renderer.setSize(window.innerWidth, window.innerHeight);
        document.body.appendChild(this.renderer.domElement);
        //this.renderer.appendChild.domElement;
        //div.appendChild(renderer.domElement);
        this.camera.position.z = 100;
        const canvas = document.createElement('canvas');
       
        var dtos = returnRows();
        console.log(dtos.lenght);
        this.createNodes(dtos);
        this.addNodesToScene(this.rootNode);
        this.addEdgesToScene(this.nodes,dtos);
        var comp = this.Graph;
        renderToCanvas({
            canvas,
            width: 120,
            height: 60,
            Component: () => <comp/>
          });
        this.renderer.render(this.scene,this.camera);
        var s = this.scene;
        var c = this.camera;
        var r = this.render;
        div = {s,c,r};
    }

    addEdgesToScene(nodes,dtos)
    {
        nodes.forEach(node => {
            let adjacentes = node.adjacents;
            adjacentes.forEach(adj => {
                dtos.forEach(dto=> {
                    if (dto.parent === adj.parent && dto.id === adj.user) {
                        this.edges.push(new Edge({
                            parent : node.parent,
                            friend : node.id,
                            ligacao : node.forcaLigacao,
                            relacao : node.forcaRelacao
                        },this.scene))
                    }
                }) 
            })
        })
    }

    createNodes(dtos){
        var j = 0;
        dtos.forEach(dto =>{
            
            var node = new Node({
                color : 0xff0000,
                radius : 3,
                user : dto.id,
                parent : dto.parent,
                adjacents : [],
                x : 5, 
                y : 5,
                angle : 0,
                angleRange : 2*Math.PI,
                depth : 0
            })
            if (dto.parent === null){
                this.rootNode = node;
            }
            this.nodes.push(node);
            j = j+5;
        });
        dtos.forEach(dto => {
            this.nodes.forEach(node =>{
                if (dto.id === node.user){
                    var adjacentes = [];
                    dtos.forEach(dto2 =>{
                        if (dto2.parent === dto.id){
                            this.nodes.forEach(node2 => {
                                if (node2.user === dto2.id){
                                    adjacentes.push(node2);
                                }
                            });
                        }
                    } );
                    node.addAdjacents(adjacentes);
                }
            });
        });
    }
    
    
    //getData() { 
        // Anda Inês, trabalha AQUIIII
        // O que está aqui não deve interessar
        /** let users = [];

        for (var i = 0; i < searchedVS.length; i++) {
            var obj = searchedVS[i];
            fetchUser(obj.requester.value);
            users.push([obj.id, requesterName, requesterEmail, obj.description.text]);
        }

        function createData(id, requester, requester_email, description) {
            return { friendshipId , id, requester, requester_email, description };
        }

        const rows = [];

        for (let i = 0; i < searchedVS.length; i += 1) {
            rows.push(createData(...users[i]));
        }

        let root;
        let nodes = [];
        users.forEach(element => {
            if (element.id === this.userRequesterId){
                var node = new Node({
                    color : 0x00ff00,
                    radius : 3,
                    user : element,
                    parent : 'undefined',
                    x : 0.0,
                    y : 0.0
                }, this.scene);
                root = node;
                nodes.push(node);
            }
        });

        let edges = [];
        
        this.addDataToScene(users, rows, root); 
    }*/


    
    addNodesToScene(node)
    {
        if (node.parent === null){
            node.initialize(this.scene);
        }
        var n = node.adjacents.lenght;
        for (var i= 0; i< node.adjacents.lenght; i++) 
        {
            var center = 0;
            if (node.parent != null){
                center = (-node.angleRange + (node.angleRange/n)) * 0.5;

                node.adjacents[i].angle = node.angle + (node.angleRange / n * i) + center;

                node.adjacents[i].angleRange = node.angleRange / n;

                var posX = 500 * (node.adjacents[i].depth) * Math.cos(node.adjacents[i].angle) - 0 * Math.sin(node.adjacents[i].angle);

                var posY = 500 * (node.adjacents[i].depth) * Math.sin(node.adjacents[i].angle) + 0 * Math.cos(node.adjacents[i].angle);

                node.adjacents[i].x = posX;
                node.adjacents[i].y = posY;
                node.adjacents[i].initialize(this.scene);
                this.addNodesToScene(node.adjacents[i]);
            }
        }
    }
        /**var length = 0;
        dtos.forEach(u => {
            if (u.parent === userId){
                length++;
            }
        })
        const slice = (2 * Math.PI) / length;
        let i=0;
        dtos.forEach(u => {
            if (u.parent === userId){
                const angle = slice * i;
                var node = new Node({
                    color : 0x0000ff,
                    radius : 3,
                    user : u.id,
                    parent : u.parent,
                    x : root.x + 6*level * Math.cos(angle), //calcular em radial
                    y : root.y + 6*level * Math.cos(angle) //calcular em radial
                })
                var edge = new Edge({
                    parent : u.parent,
                    friend : u.id,
                    ligacao : u.forcaLigacao,
                    relacao : u.forcaRelacao
                })
                i++;
            }
        })
        level++;
        
    calculateLevel()*/
    

}


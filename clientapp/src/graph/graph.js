import React, { useState, createRef, useEffect } from 'react';
import * as THREE from 'three';
import Edge from './edge';
import Node from './node';
import renderToCanvas from './renderToCanvas';


export default class Graph {
    scene;
    constructor(div){
        const { scene, renderer, camera } = this.initializeScene(div);
        
        var dtos = this.getData();
        this.createNodes(dtos);
        this.addNodesToScene(this.rootNode);
        this.addEdgesToScene(this.nodes,dtos);

        this.renderer.render();
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
        dtos.forEach(dto =>{
            var node = new Node({
                color : 0x0000ff,
                radius : 3,
                user : dto.id,
                parent : dto.parent,
                adjacents : [],
                x : 0, 
                y : 0,
                angle : 0,
                angleRange : 2*Math.PI,
                depth : 0
            })
            if (dto.parent === 'null'){
                this.rootNode = node;
            }
            this.nodes.push(node);
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

    initializeScene(div) {
        const scene = new THREE.Scene();
        this.scene = scene;
        const camera = new THREE.PerspectiveCamera(
          75,
          window.innerWidth / window.innerHeight,
          0.1,
          1000
        );
        const renderer = new THREE.WebGLRenderer();
        renderer.setSize(window.innerWidth, window.innerHeight);
        div.appendChild(renderer.domElement);
        camera.position.z = 5;
        const canvas = document.createElement('canvas');
        renderToCanvas({
            canvas,
            width: 120,
            height: 60,
            Component: () => <Graph/>
          });
        return { scene, renderer, camera };
      }

      getData (){
          
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
            node.initialize();
        }
        var n = node.adjacents.lenght;
        for (var i= 0; i< node.adjacents.lenght; i++) 
        {
            var center = 0;
            if (node.parent != null){
                center = (-node.angleRange + (node.angleRange/n)) * 0.5;

                node.adjacents[i].angle = node.angle + (node.angleRange / n * i) + center;

                node.adjacents[i].angleRange = node.angleRange / n;

                var posX = 100 * (node.adjacents[i].depth) * Math.cos(node.adjacents[i].angle) - 0 * Math.sin(node.adjacents[i].angle);

                var posY = 100 * (node.adjacents[i].depth) * Math.sin(node.adjacents[i].angle) + 0 * Math.cos(node.adjacents[i].angle);

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
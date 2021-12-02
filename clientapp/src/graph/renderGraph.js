import React from 'react';
import initializeScene from './initializeScene';
import Node from './node';
import renderToSprite from './renderToSprite';

export default async function renderGraph(div) {
  const { scene, renderer, camera } = initializeScene(div);
  const userNode = await renderToSprite(
    <Node level={0} label="Interests" />,
    {
      width: 120,
      height: 60
    }
  );
  scene.add(userNode);
  renderer.render(scene, camera);
}
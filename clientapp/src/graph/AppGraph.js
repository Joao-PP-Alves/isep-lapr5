import React, { createRef, useEffect } from 'react';
import Graph from './graph'

export default function AppGraph() {
  const divRef = createRef();
  useEffect(() => new Graph(divRef.current), [divRef]);
  return (
      <div ref={divRef} />
  );
}

import React from 'react';
import Graph from './graph';

class AppGraphContent extends React.Component {

  
    constructor(props) {
      
        super(props);
        this.canvasRef = React.createRef();
    }

    // ******************* COMPONENT LIFECYCLE ******************* //
    componentDidMount() {
        // Get canvas, pass to custom class
        const canvas = this.canvasRef.current;

        this.graph = new Graph(canvas);

    }

    componentDidUpdate(prevProps, prevState) {
        // Pass updated props to 
        const newValue = this.props.whateverProperty;
        this.Graph.updateValue(newValue);
    }

    render() {
        return (
            <div className="canvasContainer">
                <canvas ref={this.canvasRef} />
            </div>
        );
    }
}

export default function AppGraph() {
  return <AppGraphContent />;
}

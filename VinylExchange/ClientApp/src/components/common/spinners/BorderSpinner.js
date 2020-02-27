import React from 'react';

function BorderSpinner(){
    return(<div className="d-flex justify-content-center">
    <div className="spinner-border text-primary" role="status">
      <span className="sr-only">Loading...</span>
    </div>
  </div>)
}

export default BorderSpinner;
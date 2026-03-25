# React4DotNet
We'll be going step by step to create a React app that can communitcate with an API made with DotNet and C# to perform full CRUD operations.

## Create the react app
npx create-react-app React4DotNet   
cd React4DotNet
npm start

## Clear out APP.js
We're going to replace the default app with a bare-bones react app for communicating with our API.

### App.js
function App() {
  return (
    <div>
      <h1>React4DotNet</h1>
    </div>
  );
}

export default App;


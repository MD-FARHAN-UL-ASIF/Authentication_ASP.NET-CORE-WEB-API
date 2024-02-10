import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import {  createBrowserRouter,  RouterProvider,} from "react-router-dom";
import './index.css'
import Home from './components/Home/Home.jsx';
import Dashboard from './components/Dashboard/Dashboard.jsx';
import Login from './components/Login/Login.jsx';
import Registration from './components/Registration/Registration.jsx';
import CreateCategory from './components/Category/CreateCategory.jsx';
import ShowCategory from './components/ShowCategory/ShowCategory.jsx';

const router = createBrowserRouter([  {    
  path: "/",
  element: <Home></Home>
  },
  {
    path: "/dashboard",
    element: <Dashboard></Dashboard>
  },
  {
    path: "/login",
    element: <Login></Login>
  },
  {
    path: "/registration",
    element: <Registration></Registration>
  },
  {
    path: '/create-category',
    element: <CreateCategory />,
  },
  {
    path: '/show-category',
    element: <ShowCategory></ShowCategory> ,
  },
]);

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
   <RouterProvider router={router} />
  </React.StrictMode>,
)

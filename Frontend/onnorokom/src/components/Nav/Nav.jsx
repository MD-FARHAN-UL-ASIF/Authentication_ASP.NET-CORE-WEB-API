    import React from 'react';
    import { Link, NavLink } from 'react-router-dom';

    const Nav = () => {
        return (
            <div>
                <nav className='flex flex-row justify-between'>
                    <h1>OnnoRokom</h1>
                    <div className='flex justify-around'>
                        <NavLink
                            to="/"
                        >Home</NavLink>
                        
                        <NavLink
                            to="/dashboard"
                        >Dashboard</NavLink>

    <NavLink
                            to="/login"
                        >Login</NavLink>
                        
                        <NavLink
                            to="/registration"
                        >Registration</NavLink>
                        <NavLink
                            to="/create-category"
                        >Create Category</NavLink>
                    </div>
                </nav>
            </div>
        );
    };

    export default Nav;
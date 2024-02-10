// CreateCategory.jsx

import React, { useState } from 'react';
import axios from 'axios';

const CreateCategory = () => {
    const [categoryName, setCategoryName] = useState('');
    const [loading, setLoading] = useState(false);

    const handleCreateCategory = async () => {
        setLoading(true);

        const accessToken = sessionStorage.getItem('accessToken');

        try {
            const response = await axios.post(
                'https://localhost:7002/api/Category',
                { Name: categoryName },
                {
                    headers: {
                        'Content-Type': 'application/json',
                        Authorization: `Bearer ${accessToken}`,
                    },
                }
            );

            console.log(response.data);

            if (response.status === 500) {
                console.error('Internal Server Error:', response.statusText);
                alert('Category creation failed. Internal Server Error. Please try again later.');
            } else if (Object.keys(response.data).length === 0) {
                console.warn('Category creation failed:', response.statusText);
                alert('Category creation failed. Please check the provided data.');
            } else {
                alert('Category created successfully');
                // Handle success as needed
            }
        } catch (error) {
            console.error('Category Creation Failed due to:', error.message);
            alert('Category Creation Failed due to:' + error.message);
        }

        setLoading(false);
    };

    return (
        <div>
            <h2>Create Category</h2>
            <label>Category Name:</label>
            <input
                type="text"
                value={categoryName}
                onChange={(e) => setCategoryName(e.target.value)}
            />
            <button onClick={handleCreateCategory} disabled={loading}>
                Create Category
            </button>
        </div>
    );
};

export default CreateCategory;

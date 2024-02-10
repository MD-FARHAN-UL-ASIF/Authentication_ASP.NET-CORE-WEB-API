import axios from 'axios';
import React, { useEffect, useState } from 'react';

const ShowCategory = () => {
    const [categories, setCategory] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const accessToken = sessionStorage.getItem('accessToken');
                const response = await axios.get(
                    `https://localhost:7002/api/Category`,
                    {
                        headers: {
                            'Content-Type': 'application/json',
                            Authorization: `Bearer ${accessToken}`,
                        }
                    }
                );

                console.log(response.data.result);

                setCategory(response.data.result);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, []);

    return (
        <div>
            {categories.map(category => (
                <div key={category.id}>
                    <h2>{category.id}</h2>


                    
                    <h2>{category.name}</h2>
                </div>
            ))}
        </div>
    );
};

export default ShowCategory;

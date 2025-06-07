import React, { useEffect, useState } from 'react';
import { fetchMachines, fetchMachineProductionData } from './api';
import { Machine, ProductionData } from './models';
import FullScreenLoader from './components/ScreenLoader';
import ToastNotification from './components/ToastNotification';

const MachineList: React.FC = () => {
    const [machines, setMachines] = useState<Machine[]>([]);
    const [selectedMachine, setSelectedMachine] = useState<number | null>(null);
    const [productionData, setProductionData] = useState<ProductionData[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<boolean>(false);
    const [toastMessage, setToastMessage] = useState<string | null>(null);
    const [statusFilter, setStatusFilter] = useState<'all' | 'active' | 'inactive'>('all');

    useEffect(() => {
        loadMachines();
    }, []);

    const filteredMachines = machines.filter(machine => {
        if (statusFilter === 'all') return true;
        if (statusFilter === 'active') return machine.isActive;
        if (statusFilter === 'inactive') return !machine.isActive;
        return true;
    });

    const showToast = (message: string) => {
        if (toastMessage) {
            setToastMessage(null);
            setTimeout(() => setToastMessage(message), 100);
        } else {
            setToastMessage(message);
        }
    };

    const loadMachines = () => {
        setLoading(true);
        fetchMachines()
            .then(data => {
                setMachines(data);
            })
            .catch((err) => {
                console.error(err);
                setError(true);
                showToast('Error while trying to reload the machines.');
            })
            .finally(() => setLoading(false));
    };

    const handleMachineSelect = (machineId: number) => {
        setLoading(true);
        setSelectedMachine(null);
        fetchMachineProductionData(machineId)
            .then(data => {
                if (data.length == 0) {
                    setProductionData([]);
                    return;
                }
                setProductionData(data);
            })
            .catch((err) => {
                setError(true);
                showToast(`Failed to load production data for Machine #${machineId}`);
            })
            .finally(() => {
                setLoading(false)
                setSelectedMachine(machineId);
            });
    };

    return (
        <div className="machines-container">
            <FullScreenLoader show={loading} />

            {toastMessage && (
                <ToastNotification
                    message={toastMessage}
                    type="error"
                    onClose={() => setToastMessage(null)}
                />
            )}

            <div className="d-flex justify-content-between align-items-center mb-3">
                <h2 className="mb-0">Industrial Machines</h2>
                <div className="form-inline">
                    <select
                        id="statusFilter"
                        className="form-select"
                        style={{ minWidth: '150px' }}
                        value={statusFilter}
                        onChange={(e) => {
                            setStatusFilter(e.target.value as 'all' | 'active' | 'inactive')
                            setSelectedMachine(null)
                        }}>
                        <option value="all">All</option>
                        <option value="active">Active</option>
                        <option value="inactive">Inactive</option>
                    </select>
                </div>
            </div>

            <div className="card shadow-sm">
                <div className="card-body p-0">
                    <table className="table table-striped table-hover mb-0">
                        <thead className="table-light align-middle">
                            <tr>
                                <th>ID</th>
                                <th>Name</th>
                                <th>Serial Number</th>
                                <th>Type</th>
                                <th>Status</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {filteredMachines.map((machine) => (
                                <tr key={machine.id} className={machine.isActive ? 'table-success align-middle' : 'table-secondary align-middle'}>
                                    <td>{machine.id}</td>
                                    <td>{machine.name}</td>
                                    <td>{machine.serialNumber}</td>
                                    <td>{machine.type}</td>
                                    <td>
                                        <span className={`badge ${machine.isActive ? 'bg-success' : 'bg-secondary'} px-2 py-2 fs-7`} style={{ minWidth: '80px' }}>
                                            {machine.isActive ? 'Active' : 'Inactive'}
                                        </span>
                                    </td>
                                    <td>
                                        <button
                                            className="btn btn-sm btn-primary"
                                            onClick={() => handleMachineSelect(machine.id)}>
                                            View Production
                                        </button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>

            {!error && selectedMachine && productionData.length > 0 && (
                <div className="card mt-4">
                    <div className="card-header bg-primary text-white">
                        Production Data for {machines.find((m) => m.id === selectedMachine)?.name}
                    </div>
                    <div className="card-body p-0">
                        <div className="table-responsive">
                            <table className="table table-striped table-bordered mb-0">
                                <thead className="table-light">
                                    <tr>
                                        <th>Timestamp</th>
                                        <th>Efficiency (%)</th>
                                        <th>Units Produced</th>
                                        <th>Downtime (min)</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {productionData.map((data) => (
                                        <tr key={data.id}>
                                            <td>{new Date(data.timestamp).toLocaleString()}</td>
                                            <td>{data.efficiency}</td>
                                            <td>{data.unitsProduced}</td>
                                            <td>{data.downtime}</td>
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            )}

            {!loading && !error && selectedMachine && productionData.length === 0 && (
                <div className="alert alert-warning mt-4" role="alert">
                    No production data available for this machine.
                </div>
            )}

        </div>
    );
};

export default MachineList;

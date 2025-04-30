import axios from 'axios';
import { Machine, ProductionData } from './models';

const API_BASE = process.env.REACT_APP_API_BASE || 'http://localhost:5211';

export const fetchMachines = async (): Promise<Machine[]> => {
  const response = await axios.get<Machine[]>(`${API_BASE}/api/machines`);
  return response.data;
};

export const fetchMachineById = async (id: number): Promise<Machine> => {
  const response = await axios.get<Machine>(`${API_BASE}/api/machines/${id}`);
  return response.data;
};

export const fetchProductionData = async (): Promise<ProductionData[]> => {
  const response = await axios.get<ProductionData[]>(`${API_BASE}/api/production-data`);
  return response.data;
};

export const fetchMachineProductionData = async (machineId: number): Promise<ProductionData[]> => {
  const response = await axios.get<ProductionData[]>(`${API_BASE}/api/machines/${machineId}/production-data`);
  return response.data;
};

export const postProductionData = async (data: Omit<ProductionData, 'id'>): Promise<ProductionData> => {
  const response = await axios.post<ProductionData>(`${API_BASE}/api/production-data`, data);
  return response.data;
};
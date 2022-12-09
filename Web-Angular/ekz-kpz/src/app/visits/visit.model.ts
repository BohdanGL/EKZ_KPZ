import { Patient } from "../patients/patient.model";

export interface Visit {
    id?: number;
    date: string;
    time: number;
    patient?: Patient;
    patientId?: number;
}
/*
 * ME7Logger.h
 *
 * See README.txt and LICENSE.txt of ME7Logger distribution
 *
 * mki, 10/2011, 03/2013
 */

#ifndef _ME7LOGGER_H_
#define _ME7LOGGER_H_

#if defined(_WIN32) || defined(__CYGWIN__)
#define ME7LOGGERAPI    __declspec( dllimport )
#define ME7LOGGERCALL   __cdecl
#else
#define ME7LOGGERAPI
#define ME7LOGGERCALL
#endif


/* Success return value of me7logger functions */
#define ME7LOGGER_OK                            0

/* Error return values (general) */
#define ME7LOGGER_ERR_GENERAL                   (-1)
#define ME7LOGGER_ERR_INVALID_HANDLE            (-2)
#define ME7LOGGER_ERR_TOO_MANY_USERS            (-3)
#define ME7LOGGER_ERR_WRONG_STATE               (-4)

/* Error return values of me7logger_config()  */
#define ME7LOGGER_ERR_TRACE_CONFIG_NOT_FOUND    (-11)
#define ME7LOGGER_ERR_INVALID_TRACE_CONFIG      (-12)
#define ME7LOGGER_ERR_ECU_FILE_NOT_FOUND        (-13)
#define ME7LOGGER_ERR_INVALID_ECU_CONFIG        (-14)
#define ME7LOGGER_ERR_TOO_MANY_LOG_VARIABLES    (-15)
#define ME7LOGGER_ERR_LOG_VARIABLE_NOT_FOUND    (-16)

/* Error return values of me7logger_run() */
#define ME7LOGGER_ERR_INVALID_CALLBACK          (-21)
#define ME7LOGGER_ERR_INVALID_CONFIG            (-22)
#define ME7LOGGER_ERR_INVALID_INTERFACE         (-23)
#define ME7LOGGER_ERR_CONNECT_FAILED            (-24)
#define ME7LOGGER_ERR_ECUID_MISMATCH            (-25)
#define ME7LOGGER_ERR_LOGGING_FAILED            (-26)


/* Return values of the user callback function */
#define ME7LOGGER_STOP              0
#define ME7LOGGER_CONTINUE          1


/* Possible values for itf_type */
#define ME7LOGGER_ITF_SERIAL            1
#define ME7LOGGER_ITF_FTDI              2
#define ME7LOGGER_ITF_FTDI_INSTANCE     3
#define ME7LOGGER_ITF_FTDI_SERIAL       4
#define ME7LOGGER_ITF_FTDI_LOCATION     5


// Config data given to me7logger_run()
typedef struct
{
    unsigned char   itf_type;               /* ITF_SERIAL or ITF_FTDI */
    char *          itf_instance;           /* port to use for ITF_SERIAL */
                                            /* NULL for ITF_FTDI */
                                            /* instance for ITF_FTDI_INSTANCE */
                                            /* serial for ITF_FTDI_SERIAL */
                                            /* location for ITF_FTDI_LOCATION */
    unsigned int    baudrate;               /* 0 (keep default from .ecu file) or baudrate for logging */
    unsigned char   samples_per_second;     /* 0 (keep default from .cfg file) or sps (1..50) */
    unsigned char   absolute_timestamps;    /* 0 (relative) or 1 (absolute) timestamp */
    unsigned char   sync_to_full_second;    /* 0 (don't wait) or 1 (wait) for full second before logging starts */
} ME7LoggerConfiguration;


// Description for one logging variable
typedef struct
{
    const char * name;              
    const char * unit;
    const char * alias;     // Alias can be empty string if not defined
} ME7LoggerVariableDesc;


// Result description given to the user's callback function
// Also returned by me7logger_get_data_desc()
typedef struct
{
    unsigned char                   num_variables;      // Number of variables
    const ME7LoggerVariableDesc *   variables;          // Pointer to an array of variable descriptions
} ME7LoggerDataDesc;


// Absolute timestamp structure
typedef struct
{
    unsigned short year;
    unsigned short month;
    unsigned short day;
    unsigned short hour;
    unsigned short minute;
    unsigned short second;
    unsigned short millisecond;
} ME7LoggerAbsoluteTime;


// Result values given to the user's callback function
typedef struct 
{
    // Only ONE of the two following will be set (depends on setting of "absolute_timestamps" in configuration)
    unsigned long           relative_time;  // Milliseconds since start of log (or 0)
    ME7LoggerAbsoluteTime * absolute_time;  // Pointer to absolute time structure (or NULL)

    unsigned char           num_variables;  // Number of values
    const double *          values;         // Pointer to an array of values
} ME7LoggerDataValues;


// Handle for access to ME7Logger DLL functions
typedef void * ME7LoggerHandle;


#ifdef __cplusplus
extern "C"
{
#endif

// ATTENTION: the DLL functions are NOT threadsafe!
// Take care to call these functions only in a coordinated way when using multiple threads.

// Get logger handle (only one instance per process is possible)
ME7LOGGERAPI ME7LoggerHandle ME7LOGGERCALL  me7logger_open  ( void );

// Set a file to use for logging errors and information (NULL to disable output)
ME7LOGGERAPI int ME7LOGGERCALL  me7logger_set_logfile ( ME7LoggerHandle handle,             // Handle from me7logger_open()
                                                        FILE * fp );                        // File pointer or NULL

// Configure the logger (reads trace config and ecu characteristics)
ME7LOGGERAPI int ME7LOGGERCALL  me7logger_config ( ME7LoggerHandle  handle,                 // Handle from me7logger_open()
                                                   const char *     program_name,           // Program name (use argv[0])
                                                   const char *     trace_config_file );    // Filename of trace config

// Get result data description (possible only after configuration)
ME7LOGGERAPI ME7LoggerDataDesc * ME7LOGGERCALL  me7logger_get_data_desc ( ME7LoggerHandle handle ); // Handle from me7logger_open()


// The user's callback function to receive log data description and log data values
typedef int (*ME7LoggerCallback)(ME7LoggerHandle             handle,            // Handle from me7logger_open()
                                 const ME7LoggerDataDesc *   data_desc,         // Pointer to result data description
                                 const ME7LoggerDataValues * data_values );     // Pointer to result data values

// Connect the logger and run logging session
// This function calls the callback function to deliver results
// This function returns only when connection to ecu failed
// or when the callback function returned stop value!!
ME7LOGGERAPI int ME7LOGGERCALL  me7logger_run    ( ME7LoggerHandle                handle,
                                                   const ME7LoggerConfiguration * config,
                                                   ME7LoggerCallback              callback );

// Release logger handle
ME7LOGGERAPI int ME7LOGGERCALL  me7logger_close ( ME7LoggerHandle handle );

#ifdef __cplusplus
}
#endif

#endif  //_ME7LOGGER_H_

